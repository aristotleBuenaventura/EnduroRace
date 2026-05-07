using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Component.Transforming;
using UnityEngine;
using System.Collections;
using Firebase.Firestore;
using Firebase.Extensions;

public class NetworkPlayer : NetworkBehaviour
{
    [HideInInspector] public Vector2 serverMoveInput;
    [HideInInspector] public bool serverSprint;

    [ServerRpc]
    public void ServerSetInput(Vector2 move, bool sprint)
    {
        serverMoveInput = move;
        serverSprint = sprint;
    }

    [ServerRpc]
    public void ServerSetRunnerAnimState(byte state)
    {
        RunnerAnimState.Value = state;
    }

    [ServerRpc(RequireOwnership = true)]
    public void ServerSetSwimStrokeStyle(byte index)
    {
        SwimStrokeStyleIndex.Value = (byte)(index % 3);
    }

    [ServerRpc]
    public void ServerSetCyclistAnimState(byte state)
    {
        CyclistAnimState.Value = state;
    }

    public readonly SyncVar<bool>   IsOnBike         = new();
    public readonly SyncVar<float>  Stamina          = new();
    public readonly SyncVar<bool>   IsRunning        = new();
    public readonly SyncVar<bool>   CanMove          = new(true);
    public readonly SyncVar<bool>   IsTripping       = new();
    public readonly SyncVar<float>  TripEndTime      = new();
    public readonly SyncVar<bool>   IsInMud          = new();
    public readonly SyncVar<float>  MudMultiplier    = new(1f);
    public readonly SyncVar<bool>   IsInWater        = new();
    public readonly SyncVar<float>  WaterSurfaceY    = new();
    public readonly SyncVar<bool>   IsStunned        = new();
    public readonly SyncVar<byte>   RunnerAnimState  = new();
    public readonly SyncVar<byte>   CyclistAnimState = new();
    /// <summary>0 = isSwimming, 1 = isSwimming2, 2 = isSwimming3. Set from profile selection.</summary>
    public readonly SyncVar<byte>   SwimStrokeStyleIndex = new SyncVar<byte>(0);
    public readonly SyncVar<string> PlayerName       = new SyncVar<string>("");

    // Bridges Firebase UID → NetworkPlayer so RacePlayerTracker can look up real names.
    // Set by the owning client on spawn via CmdSetFirebaseId, then broadcast to all.
    public readonly SyncVar<string> FirebasePlayerId = new SyncVar<string>("");

    // ── RTT sync so non-owners can use the real round-trip time for prediction ──
    public readonly SyncVar<float> NetworkRTT = new SyncVar<float>(0.1f);

    private readonly SyncVar<Vector3>    syncedPosition = new();
    private readonly SyncVar<Quaternion> syncedRotation = new();
    private readonly SyncVar<Vector3>    syncedVelocity = new();

    private Vector3    targetPosition;
    private Quaternion targetRotation;
    private Vector3    currentVelocity;

    [SerializeField] private float interpolationSpeed     = 20f;
    [SerializeField] private float fastInterpolationSpeed = 30f;

    private const float positionDeadzone  = 0.005f;
    private const float teleportThreshold = 8f;

    private float syncTimer = 0f;
    private const float syncInterval      = 0.010f;
    private const float waterSyncInterval = 0.016f;

    private bool  _wasInWater    = false;
    private float rttUpdateTimer = 0f;

    private SegmentSwitcher  segmentSwitcher;
    private PlayerController playerController;
    private Vector3          lastPosition;

    // ── Server ──────────────────────────────────────────────────────────────────

    public override void OnStartServer()
    {
        base.OnStartServer();

        Stamina.Value          = 100f;
        IsRunning.Value        = false;
        IsTripping.Value       = false;
        IsOnBike.Value         = false;
        CanMove.Value          = true;
        IsInWater.Value        = false;
        IsStunned.Value        = false;
        PlayerName.Value       = "";
        FirebasePlayerId.Value = "";
        NetworkRTT.Value       = 0.1f;
    }

    // ── Client ──────────────────────────────────────────────────────────────────

    public override void OnStartClient()
    {
        base.OnStartClient();

        segmentSwitcher  = GetComponent<SegmentSwitcher>();
        playerController = GetComponentInChildren<PlayerController>();

        syncedPosition.OnChange   += OnPositionChanged;
        syncedRotation.OnChange   += OnRotationChanged;
        syncedVelocity.OnChange   += OnVelocityChanged;
        RunnerAnimState.OnChange  += OnRunnerAnimStateChanged;
        CyclistAnimState.OnChange += OnCyclistAnimStateChanged;
        SwimStrokeStyleIndex.OnChange += OnSwimStrokeStyleIndexChanged;
        WaterSurfaceY.OnChange    += OnWaterSurfaceYChanged;

        // When FirebasePlayerId arrives (or updates), notify the tracker on all machines
        FirebasePlayerId.OnChange += OnFirebasePlayerIdChanged;

        targetPosition = transform.position;
        targetRotation = transform.rotation;

        if (!IsOwner)
        {
            foreach (var cc in GetComponentsInChildren<CharacterController>())
                cc.enabled = false;
        }

        if (!IsValidRotation(targetRotation))
        {
            targetRotation     = Quaternion.identity;
            transform.rotation = Quaternion.identity;
        }

        lastPosition = transform.position;
        _wasInWater  = IsInWater.Value;

        ;

        if (IsOwner)
        {
            byte localSwimStyle = (byte)Mathf.Clamp(PlayerPrefs.GetInt("SelectedSwimStrokeStyle", 0), 0, 2);
            ServerSetSwimStrokeStyle(localSwimStyle);

            // Send Firebase UID to server so RacePlayerTracker can resolve real names
            StartCoroutine(SendFirebaseIdToServer());

            // Send selected model to server
            StartCoroutine(FetchModelAndSendToServer());
        }

        // If FirebasePlayerId is already populated by the time OnStartClient fires
        // (e.g. BufferLast or fast host path), link immediately
        if (!string.IsNullOrEmpty(FirebasePlayerId.Value))
        {
            var tracker = FindFirstObjectByType<RacePlayerTracker>();
            tracker?.TryLinkByFirebaseId(this, FirebasePlayerId.Value);
        }
    }

    private void OnDestroy()
    {
        syncedPosition.OnChange   -= OnPositionChanged;
        syncedRotation.OnChange   -= OnRotationChanged;
        syncedVelocity.OnChange   -= OnVelocityChanged;
        RunnerAnimState.OnChange  -= OnRunnerAnimStateChanged;
        CyclistAnimState.OnChange -= OnCyclistAnimStateChanged;
        SwimStrokeStyleIndex.OnChange -= OnSwimStrokeStyleIndexChanged;
        WaterSurfaceY.OnChange    -= OnWaterSurfaceYChanged;
        FirebasePlayerId.OnChange -= OnFirebasePlayerIdChanged;
    }

    // ── Firebase ID sync ─────────────────────────────────────────────────────────

    /// <summary>
    /// Waits for Firebase to be ready, then sends the local player's Firebase UID
    /// to the server so RacePlayerTracker can match it to the lobby display name.
    /// </summary>
    private IEnumerator SendFirebaseIdToServer()
    {
        if (!FirebaseManager.Instance.IsFirebaseReady)
            yield return new WaitUntil(() => FirebaseManager.Instance.IsFirebaseReady);

        string firebaseId = FirebaseManager.Instance.PlayerId;

        if (!string.IsNullOrEmpty(firebaseId))
        {
            ;
            CmdSetFirebaseId(firebaseId);
        }
        else
        {
            ;
        }
    }

    [ServerRpc(RequireOwnership = true)]
    private void CmdSetFirebaseId(string id)
    {
        FirebasePlayerId.Value = id;
        ;
    }

    /// <summary>
    /// SyncVar callback — fires on server and every client whenever FirebasePlayerId changes.
    /// Notifies RacePlayerTracker so the name is linked as soon as the UID arrives,
    /// even if InitializeRacers already ran.
    /// </summary>
    private void OnFirebasePlayerIdChanged(string prev, string next, bool asServer)
    {
        if (string.IsNullOrEmpty(next)) return;

        var tracker = FindFirstObjectByType<RacePlayerTracker>();
        tracker?.TryLinkByFirebaseId(this, next);

        ;
    }

    // ── Model sync ───────────────────────────────────────────────────────────────

    [ServerRpc(RequireOwnership = true)]
    public void ServerSendModel(string modelName)
    {
        ;

        NetworkStarter starter = FindFirstObjectByType<NetworkStarter>();
        if (starter != null)
            starter.RegisterClientModel(Owner, modelName);
        else
            ;
    }

    private IEnumerator FetchModelAndSendToServer()
    {
        if (!FirebaseManager.Instance.IsFirebaseReady)
            yield return new WaitUntil(() => FirebaseManager.Instance.IsFirebaseReady);

        var db       = FirebaseManager.Instance.Db;
        var playerId = FirebaseManager.Instance.PlayerId;

        string model = PlayerPrefs.GetString("SelectedModel", "Male");
        byte swimStyle = (byte)Mathf.Clamp(PlayerPrefs.GetInt("SelectedSwimStrokeStyle", 0), 0, 2);
        bool done    = false;

        ;
        ServerSendModel(model);
        ServerSetSwimStrokeStyle(swimStyle);

        db.Collection("players").Document(playerId).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled && task.Result.Exists)
                {
                    if (task.Result.ContainsField("selectedModel"))
                    {
                        string firebaseModel = task.Result.GetValue<string>("selectedModel");
                        PlayerPrefs.SetString("SelectedModel", firebaseModel);
                        if (firebaseModel != model)
                        {
                            ;
                            ServerSendModel(firebaseModel);
                        }
                    }

                    if (task.Result.ContainsField("swimStrokeStyle"))
                    {
                        int firebaseSwimStyle = Mathf.Clamp(task.Result.GetValue<int>("swimStrokeStyle"), 0, 2);
                        PlayerPrefs.SetInt("SelectedSwimStrokeStyle", firebaseSwimStyle);
                        ServerSetSwimStrokeStyle((byte)firebaseSwimStyle);
                    }

                    PlayerPrefs.Save();
                }
                done = true;
            });

        yield return new WaitUntil(() => done);
    }

    // ── SyncVar change callbacks ─────────────────────────────────────────────────

    private void OnVelocityChanged(Vector3 prev, Vector3 next, bool asServer)
    {
        if (!IsOwner) currentVelocity = next;
    }

    private void OnWaterSurfaceYChanged(float prev, float next, bool asServer)
    {
        if (!IsOwner && playerController != null)
        {
            playerController.waterSurfaceY = next;
            ;
        }
    }

    private void OnRunnerAnimStateChanged(byte prev, byte next, bool asServer)
    {
        if (!IsOwner && playerController != null)
            playerController.ApplyAnimationStateFromNetwork(next);
    }

    private void OnSwimStrokeStyleIndexChanged(byte prev, byte next, bool asServer)
    {
        if (playerController == null)
            return;
        byte s = RunnerAnimState.Value;
        if (s == 3 || s == 4)
            playerController.ApplyAnimationStateFromNetwork(s);
    }

    private void OnCyclistAnimStateChanged(byte prev, byte next, bool asServer)
    {
        if (!IsOwner)
        {
            CyclingController cyclist = GetComponentInChildren<CyclingController>();
            if (cyclist != null)
                cyclist.ApplyAnimationStateFromNetwork(next);
        }
    }

    private void OnPositionChanged(Vector3 prev, Vector3 next, bool asServer)
    {
        if (!IsOwner) targetPosition = next;
    }

    private void OnRotationChanged(Quaternion prev, Quaternion next, bool asServer)
    {
        if (!IsOwner) targetRotation = next;
    }

    // ── Update ───────────────────────────────────────────────────────────────────

    private void Update()
    {
        if (IsServerInitialized && IsTripping.Value && Time.time >= TripEndTime.Value)
        {
            IsTripping.Value = false;
            ;
        }

        if (IsOwner)
        {
            bool nowInWater = IsInWater.Value;
            if (nowInWater != _wasInWater)
            {
                _wasInWater = nowInWater;
                syncTimer   = float.MaxValue;
            }

            syncTimer += Time.deltaTime;
            float currentSyncInterval = IsInWater.Value ? waterSyncInterval : syncInterval;

            if (syncTimer >= currentSyncInterval)
            {
                float elapsed = Mathf.Min(syncTimer, 0.2f);
                syncTimer     = 0f;

                Vector3 vel  = (transform.position - lastPosition) / elapsed;
                lastPosition = transform.position;

                ServerSyncTransform(transform.position, transform.rotation, vel);
            }

            rttUpdateTimer += Time.deltaTime;
            if (rttUpdateTimer >= 1f)
            {
                rttUpdateTimer = 0f;
                float measuredRTT = (float)NetworkManager.TimeManager.RoundTripTime / 1000f;
                ServerUpdateRTT(measuredRTT);
            }
        }
        else
        {
            InterpolatePosition();
            InterpolateRotation();

            PlayerController pc = GetComponentInChildren<PlayerController>();
            if (pc != null)
                pc.transform.localPosition = Vector3.zero;

            CyclingController cc = GetComponentInChildren<CyclingController>();
            if (cc != null)
                cc.transform.localPosition = Vector3.zero;
        }
    }

    // ── RTT sync ─────────────────────────────────────────────────────────────────

    [ServerRpc]
    private void ServerUpdateRTT(float rtt)
    {
        NetworkRTT.Value = Mathf.Clamp(rtt, 0.03f, 0.5f);
    }

    // ── Position interpolation ───────────────────────────────────────────────────

    private void InterpolatePosition()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance >= teleportThreshold)
        {
            transform.position = targetPosition;
            currentVelocity    = Vector3.zero;
            return;
        }

        if (distance < positionDeadzone) return;

        if (IsInWater.Value)
        {
            float waterSpeed = Mathf.Lerp(20f, 45f, Mathf.Clamp01(distance / 1.5f));
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                waterSpeed * distance * Time.deltaTime
            );
        }
        else
        {
            float speed = Mathf.Lerp(interpolationSpeed, fastInterpolationSpeed,
                                     Mathf.Clamp01((distance - 0.5f) / 1.5f));

            float rttEstimate = Mathf.Clamp(NetworkRTT.Value * 0.5f, syncInterval, 0.3f);
            Vector3 predicted = targetPosition + currentVelocity * rttEstimate;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                predicted,
                ref currentVelocity,
                0.05f,
                fastInterpolationSpeed,
                Time.deltaTime
            );
        }

        if (Vector3.Distance(transform.position, targetPosition) < positionDeadzone)
            transform.position = targetPosition;
    }

    // ── Rotation interpolation ───────────────────────────────────────────────────

    private void InterpolateRotation()
    {
        if (!IsValidRotation(targetRotation) || !IsValidRotation(transform.rotation))
            return;

        float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);

        if (angleDiff < 0.1f)
        {
            transform.rotation = targetRotation;
            return;
        }

        float speed = angleDiff > 45f ? fastInterpolationSpeed : interpolationSpeed;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * speed
        );
    }

    private bool IsValidRotation(Quaternion q)
        => !(q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0);

    // ── Server RPCs ──────────────────────────────────────────────────────────────

    [ServerRpc]
    private void ServerSyncTransform(Vector3 pos, Quaternion rot, Vector3 vel)
    {
        syncedPosition.Value = pos;
        syncedRotation.Value = rot;
        syncedVelocity.Value = vel;
    }

    public void SetOnBike(bool value)
    {
        if (!IsOwner) return;
        ServerSetOnBike(value);
    }

    public void SetCanMove(bool value)
    {
        if (!IsOwner) return;
        ServerSetCanMove(value);
    }

    [ServerRpc]
    private void ServerSetOnBike(bool value) => IsOnBike.Value = value;

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetCanMove(bool value) => CanMove.Value = value;

    [ServerRpc(RequireOwnership = false)]
    public void ServerTrip(float duration)
    {
        IsTripping.Value  = true;
        TripEndTime.Value = Time.time + duration;
        ;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerEnterMud(float multiplier)
    {
        IsInMud.Value       = true;
        MudMultiplier.Value = multiplier;
        ;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerExitMud()
    {
        IsInMud.Value       = false;
        MudMultiplier.Value = 1f;
        ;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetStunned(bool stunned) => IsStunned.Value = stunned;

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetInWater(bool inWater, float surfaceY = 0f)
    {
        IsInWater.Value     = inWater;
        WaterSurfaceY.Value = surfaceY;

        if (inWater && playerController != null)
            playerController.waterSurfaceY = surfaceY;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetPlayerName(string name) => PlayerName.Value = name;

    // ── Spawn confirmation ───────────────────────────────────────────────────────

    [ObserversRpc]
    public void RpcConfirmSpawnPosition(Vector3 pos, Quaternion rot)
    {
        if (IsOwner) return;

        transform.position = pos;
        transform.rotation = rot;
        targetPosition     = pos;
        targetRotation     = rot;
        currentVelocity    = Vector3.zero;
        ;
    }

    // ── Helpers ──────────────────────────────────────────────────────────────────

    public bool CanPlayerMove =>
        CanMove.Value &&
        !IsTripping.Value &&
        !IsStunned.Value;

    public bool OnBike => IsOnBike.Value;

    public SegmentSwitcher GetLocalSegmentSwitcher()
    {
        if (!IsOwner) return null;
        return segmentSwitcher;
    }
}