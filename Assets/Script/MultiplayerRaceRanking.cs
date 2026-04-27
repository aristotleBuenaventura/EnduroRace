using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Transporting;

public class MultiplayerRaceRanking : NetworkBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector
    // -------------------------------------------------------------------------
    [Header("UI References")]
    public GameObject rankingPanel;
    public Transform rankingContainer;
    public GameObject rankEntryPrefab;

    [Header("Checkpoint System")]
    public Transform[] checkpoints;
    public Transform finishLine;

    [Header("AI System")]
    public MonoBehaviour aiManager;

    [Header("Update Settings")]
    public float broadcastInterval = 0.2f;

    [Header("Display Settings")]
    public int maxDisplayedRanks = 10;
    public Color localPlayerColor = Color.yellow;
    public Color otherPlayerColor = Color.red;
    public Color aiColor = Color.gray;

    // -------------------------------------------------------------------------
    // Internal types
    // -------------------------------------------------------------------------
    public enum RacerType { NetworkPlayer, AI }

    private class RacerData
    {
        public string name;
        public RacerType type;
        public NetworkPlayer networkPlayer;
        public object aiOpponent;
        public bool isLocalPlayer;

        public int checkpointsPassed = 0;
        public float totalProgress = 0f;
        public int rank = 0;
        public float finishTime = 0f;
        public Vector3 segmentStartPos;
        public float segmentLength;

        public Transform GetTransform()
        {
            if (type == RacerType.NetworkPlayer && networkPlayer != null)
                return networkPlayer.transform;
            if (type == RacerType.AI && aiOpponent != null)
            {
                var p = aiOpponent.GetType().GetProperty("transform");
                return p?.GetValue(aiOpponent) as Transform;
            }
            return null;
        }
    }

    public struct RankSnapshot
    {
        public string name;
        public int rank;
        public float totalProgress;
        public bool isLocalPlayer;
        public RacerType racerType;
        public int ownerId;
        public float finishTime;
    }

    private class RankEntryUI
    {
        public GameObject gameObject;
        public TMP_Text rankText;
        public TMP_Text nameText;
        public TMP_Text gapText;
    }

    // -------------------------------------------------------------------------
    // State
    // -------------------------------------------------------------------------

    private List<RacerData> racers = new List<RacerData>();
    private RankSnapshot[] latestSnapshot = System.Array.Empty<RankSnapshot>();

    // FIX: Key UI entries by a unique string that won't collide between players/AI
    // Format: "p_{ownerId}" for players, "ai_{name}" for AI
    private Dictionary<string, RankEntryUI> rankEntries = new Dictionary<string, RankEntryUI>();

    private float broadcastTimer = 0f;

    // FIX: Cache local owner ID once instead of searching every broadcast tick
    private int _cachedLocalOwnerId = -1;

    // -------------------------------------------------------------------------
    // Lifecycle
    // -------------------------------------------------------------------------

    public override void OnStartServer()
    {
        base.OnStartServer();
        FishNet.InstanceFinder.ServerManager.OnRemoteConnectionState += OnClientConnectionChanged;

        // FIX: Also initialize on server start so the host is registered immediately
        Invoke(nameof(InitializeRacers), 1f);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        FishNet.InstanceFinder.ServerManager.OnRemoteConnectionState -= OnClientConnectionChanged;
    }

    private void OnClientConnectionChanged(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
            // FIX: Use coroutine to wait until tracker is ready instead of a fixed 0.5s delay
            StartCoroutine(InitializeRacersWhenReady());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (rankingPanel != null) rankingPanel.SetActive(true);

        // FIX: Cache local OwnerId once on client start — avoids FindObjectsByType every broadcast tick
        StartCoroutine(CacheLocalOwnerIdWhenReady());
    }

    // FIX: Wait until the local NetworkPlayer has spawned and is owner before caching
    private IEnumerator CacheLocalOwnerIdWhenReady()
    {
        float timeout = 5f;
        float elapsed = 0f;

        while (elapsed < timeout)
        {
            NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
            foreach (var np in players)
            {
                if (np.IsOwner)
                {
                    _cachedLocalOwnerId = np.OwnerId;
                    ;
                    yield break;
                }
            }

            elapsed += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        ;
    }

    private void LateUpdate()
    {
        if (IsServerStarted)
        {
            foreach (var racer in racers)
                CalculateProgress(racer);

            broadcastTimer += Time.deltaTime;
            if (broadcastTimer >= broadcastInterval)
            {
                SortAndBroadcast();
                broadcastTimer = 0f;
            }
        }

        if (IsClientStarted)
            UpdateUI();
    }

    // -------------------------------------------------------------------------
    // Server: initialisation
    // -------------------------------------------------------------------------

    // FIX: Wait until RacePlayerTracker has data before initializing,
    // instead of using a fixed 0.5s delay which is too short on real networks
    private IEnumerator InitializeRacersWhenReady()
    {
        RacePlayerTracker tracker = null;
        float timeout = 5f;
        float elapsed = 0f;

        while (elapsed < timeout)
        {
            tracker = FindFirstObjectByType<RacePlayerTracker>();
            if (tracker != null && tracker.IsDataReady())
                break;

            elapsed += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }

        if (tracker == null)
            ;
        else if (!tracker.IsDataReady())
            ;

        InitializeRacers();
    }

    [Server]
    private void InitializeRacers()
    {
        racers.Clear();
        ;

        NetworkPlayer[] allPlayers = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        ;

        RacePlayerTracker tracker = FindFirstObjectByType<RacePlayerTracker>();

        // FIX: Log all tracker entries so you can verify OwnerId-to-name mapping
        if (tracker != null)
        {
            tracker.DebugLogAllPlayers();
        }
        else
        {
            ;
        }

        foreach (var netPlayer in allPlayers)
        {
            if (racers.Any(r => r.type == RacerType.NetworkPlayer
                            && r.networkPlayer?.OwnerId == netPlayer.OwnerId))
                continue;

            string playerName = $"Player {netPlayer.OwnerId}"; // stable fallback

            if (tracker != null)
            {
                var pd = tracker.GetPlayerDataByOwnerId(netPlayer.OwnerId);
                if (pd != null && !string.IsNullOrEmpty(pd.displayName))
                    playerName = pd.displayName;
                else
                    ;
            }

            Vector3 startPos = netPlayer.transform.position;
            float firstSegLen = (checkpoints != null && checkpoints.Length > 0)
                ? Vector3.Distance(startPos, checkpoints[0].position)
                : 0f;

            racers.Add(new RacerData
            {
                name            = playerName,
                type            = RacerType.NetworkPlayer,
                networkPlayer   = netPlayer,
                isLocalPlayer   = false,
                segmentStartPos = startPos,
                segmentLength   = firstSegLen,
            });

            ;
        }

        if (aiManager != null)
        {
            var method = aiManager.GetType().GetMethod("GetActiveOpponents");
            if (method != null)
            {
                var opponents = method.Invoke(aiManager, null) as System.Collections.IList;
                if (opponents != null)
                {
                    foreach (var aiObj in opponents)
                    {
                        if (aiObj == null) continue;
                        string aiName = aiObj.GetType().GetProperty("opponentName")?.GetValue(aiObj) as string ?? "AI";
                        Transform aiTransform = aiObj.GetType().GetProperty("transform")?.GetValue(aiObj) as Transform;

                        if (aiTransform != null)
                        {
                            Vector3 aiStartPos = aiTransform.position;
                            float aiSegLen = (checkpoints != null && checkpoints.Length > 0)
                                ? Vector3.Distance(aiStartPos, checkpoints[0].position)
                                : 0f;

                            racers.Add(new RacerData
                            {
                                name            = aiName,
                                type            = RacerType.AI,
                                aiOpponent      = aiObj,
                                isLocalPlayer   = false,
                                segmentStartPos = aiStartPos,
                                segmentLength   = aiSegLen,
                            });
                            ;
                        }
                    }
                }
            }
        }

        ;
        SortAndBroadcast();
    }

    // -------------------------------------------------------------------------
    // Server: progress calculation & ranking
    // -------------------------------------------------------------------------

    [Server]
    private void CalculateProgress(RacerData racer)
    {
        Transform t = racer.GetTransform();
        if (t == null) return;

        if (checkpoints == null || checkpoints.Length == 0)
        {
            if (finishLine != null)
                racer.totalProgress = 10000f - Vector3.Distance(t.position, finishLine.position);
            return;
        }

        float checkpointProgress = racer.checkpointsPassed * 1000f;
        float segmentProgress = 0f;
        int nextCP = racer.checkpointsPassed;

        if (nextCP < checkpoints.Length)
        {
            if (racer.segmentLength <= 0f)
                racer.segmentLength = Vector3.Distance(racer.segmentStartPos, checkpoints[nextCP].position);

            if (racer.segmentLength > 0f)
            {
                Vector3 dir = (checkpoints[nextCP].position - racer.segmentStartPos).normalized;
                float fwd = Mathf.Max(0f, Vector3.Dot(t.position - racer.segmentStartPos, dir));
                segmentProgress = Mathf.Clamp01(fwd / racer.segmentLength) * 999f;
            }
        }

        racer.totalProgress = checkpointProgress + segmentProgress;
    }

    [Server]
    private void SortAndBroadcast()
    {
        if (racers.Count == 0) return;

        racers.Sort((a, b) => b.totalProgress.CompareTo(a.totalProgress));
        for (int i = 0; i < racers.Count; i++)
            racers[i].rank = i + 1;

        // FIX: Re-resolve names every broadcast so late-arriving tracker data
        // corrects any names that were wrong at init time
        RacePlayerTracker tracker = FindFirstObjectByType<RacePlayerTracker>();

        var snapshot = racers.Take(maxDisplayedRanks).Select(r =>
        {
            string resolvedName = r.name;

            if (tracker != null && r.type == RacerType.NetworkPlayer && r.networkPlayer != null)
            {
                var pd = tracker.GetPlayerDataByOwnerId(r.networkPlayer.OwnerId);
                if (pd != null && !string.IsNullOrEmpty(pd.displayName))
                {
                    resolvedName = pd.displayName;
                    // Also update the stored name so future snapshots are consistent
                    r.name = resolvedName;
                }
            }

            return new RankSnapshot
            {
                name          = resolvedName,
                rank          = r.rank,
                totalProgress = r.totalProgress,
                isLocalPlayer = false,
                racerType     = r.type,
                ownerId       = r.type == RacerType.NetworkPlayer && r.networkPlayer != null
                                ? r.networkPlayer.OwnerId : -1,
                finishTime    = r.finishTime,
            };
        }).ToArray();

        RpcReceiveRankings(snapshot);
    }

    // -------------------------------------------------------------------------
    // RPC: server → all clients
    // -------------------------------------------------------------------------

    [ObserversRpc(BufferLast = true)]
    private void RpcReceiveRankings(RankSnapshot[] snapshot)
    {
        // FIX: Use cached OwnerId instead of searching every tick
        for (int i = 0; i < snapshot.Length; i++)
            snapshot[i].isLocalPlayer = (snapshot[i].ownerId == _cachedLocalOwnerId);

        latestSnapshot = snapshot;
    }

    // -------------------------------------------------------------------------
    // Checkpoint reporting
    // -------------------------------------------------------------------------

    public void ReportCheckpointPassed(NetworkPlayer networkPlayer, int checkpointIndex)
    {
        ServerRpcCheckpoint(networkPlayer.OwnerId, checkpointIndex);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerRpcCheckpoint(int ownerId, int checkpointIndex)
    {
        var racer = racers.FirstOrDefault(r =>
            r.type == RacerType.NetworkPlayer &&
            r.networkPlayer != null &&
            r.networkPlayer.OwnerId == ownerId);

        if (racer == null)
        {
            ;
            return;
        }

        ServerUpdateCheckpoint(racer, checkpointIndex);
    }

    // -------------------------------------------------------------------------
    // AI checkpoint (called directly on host)
    // -------------------------------------------------------------------------

    public void OnAICheckpointPassed(Transform aiTransform, int checkpointIndex)
    {
        if (!IsServerStarted) return;

        var racer = racers.FirstOrDefault(r =>
            r.type == RacerType.AI && r.GetTransform() == aiTransform);

        if (racer == null)
        {
            NetworkedAIOpponent ai = aiTransform.GetComponent<NetworkedAIOpponent>()
                ?? aiTransform.GetComponentInParent<NetworkedAIOpponent>();

            if (ai != null)
                racer = racers.FirstOrDefault(r => r.type == RacerType.AI && r.aiOpponent == (object)ai);

            if (racer == null && ai != null)
            {
                Vector3 aiStartPos = aiTransform.position;
                float aiSegLen = (checkpoints != null && checkpoints.Length > 0)
                    ? Vector3.Distance(aiStartPos, checkpoints[0].position)
                    : 0f;

                racer = new RacerData
                {
                    name            = ai.opponentName,
                    type            = RacerType.AI,
                    aiOpponent      = ai,
                    isLocalPlayer   = false,
                    segmentStartPos = aiStartPos,
                    segmentLength   = aiSegLen,
                };
                racers.Add(racer);
                ;
            }
        }

        if (racer != null)
            ServerUpdateCheckpoint(racer, checkpointIndex);
        else
            ;
    }

    // -------------------------------------------------------------------------
    // Shared checkpoint update
    // -------------------------------------------------------------------------

    [Server]
    private void ServerUpdateCheckpoint(RacerData racer, int checkpointIndex)
    {
        if (checkpointIndex < racer.checkpointsPassed)
        {
            ;
            return;
        }

        racer.checkpointsPassed = checkpointIndex + 1;
        Transform t = racer.GetTransform();

        // FIX: Only update segment data if we have a valid transform
        // Previously this fell back to Vector3.zero which corrupted progress permanently
        if (t != null)
        {
            racer.segmentStartPos = t.position;

            int nextCP = racer.checkpointsPassed;
            racer.segmentLength = nextCP < checkpoints.Length
                ? Vector3.Distance(racer.segmentStartPos, checkpoints[nextCP].position)
                : 0f;
        }
        else
        {
            ;
        }

        string tag = racer.type == RacerType.NetworkPlayer ? "PLAYER" : "AI";
        ;

        SortAndBroadcast();
    }

    // -------------------------------------------------------------------------
    // Client: UI rendering
    // -------------------------------------------------------------------------

    private void UpdateUI()
    {
        if (rankingContainer == null || rankEntryPrefab == null || latestSnapshot.Length == 0)
            return;

        // FIX: Use unique composite key instead of name to avoid collisions
        // between players/AI with same display name
        var activeKeys = latestSnapshot.Select(GetSnapshotKey).ToHashSet();

        var toRemove = rankEntries.Keys.Where(k => !activeKeys.Contains(k)).ToList();
        foreach (var key in toRemove)
        {
            if (rankEntries[key].gameObject != null)
                Destroy(rankEntries[key].gameObject);
            rankEntries.Remove(key);
        }

        for (int i = 0; i < latestSnapshot.Length; i++)
        {
            var snap = latestSnapshot[i];
            string key = GetSnapshotKey(snap);

            if (!rankEntries.ContainsKey(key))
                CreateRankEntry(key);

            UpdateRankEntry(key, snap, latestSnapshot[0].totalProgress);
            rankEntries[key].gameObject.transform.SetSiblingIndex(i);
        }
    }

    // FIX: Unique key per racer — players by OwnerId, AI by name
    private string GetSnapshotKey(RankSnapshot snap)
        => snap.racerType == RacerType.NetworkPlayer ? $"p_{snap.ownerId}" : $"ai_{snap.name}";

    // -------------------------------------------------------------------------
    // Finish reporting
    // -------------------------------------------------------------------------

    public void ReportFinish(NetworkPlayer networkPlayer, float time)
    {
        ServerRpcFinish(networkPlayer.OwnerId, time);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerRpcFinish(int ownerId, float time)
    {
        var racer = racers.FirstOrDefault(r =>
            r.type == RacerType.NetworkPlayer &&
            r.networkPlayer != null &&
            r.networkPlayer.OwnerId == ownerId);

        if (racer != null)
            racer.finishTime = time;
    }

    public void ReportAIFinish(Transform aiTransform, float time)
    {
        if (!IsServerStarted) return;
        var racer = racers.FirstOrDefault(r => r.type == RacerType.AI && r.GetTransform() == aiTransform);
        if (racer != null)
            racer.finishTime = time;
    }

    // -------------------------------------------------------------------------
    // UI helpers
    // -------------------------------------------------------------------------

    private void CreateRankEntry(string key)
    {
        GameObject go = Instantiate(rankEntryPrefab, rankingContainer);
        rankEntries[key] = new RankEntryUI
        {
            gameObject = go,
            rankText   = go.transform.Find("RankText")?.GetComponent<TMP_Text>(),
            nameText   = go.transform.Find("NameText")?.GetComponent<TMP_Text>(),
            gapText    = go.transform.Find("GapText")?.GetComponent<TMP_Text>()
        };
    }

    private void UpdateRankEntry(string key, RankSnapshot snap, float leaderProgress)
    {
        if (!rankEntries.TryGetValue(key, out var entry)) return;

        Color color = snap.isLocalPlayer ? localPlayerColor
                    : snap.racerType == RacerType.AI ? aiColor
                    : otherPlayerColor;

        if (entry.rankText != null) { entry.rankText.text = GetRankSuffix(snap.rank); entry.rankText.color = color; }
        if (entry.nameText != null) { entry.nameText.text = snap.name; entry.nameText.color = color; }

        if (entry.gapText != null)
        {
            if (snap.rank == 1)
            {
                entry.gapText.text  = "LEAD";
                entry.gapText.color = Color.green;
            }
            else
            {
                float gap = (leaderProgress - snap.totalProgress) / 1000f;
                entry.gapText.text  = $"+{gap:F2}";
                entry.gapText.color = color;
            }
        }
    }

    // -------------------------------------------------------------------------
    // Helpers / public API
    // -------------------------------------------------------------------------

    private string GetRankSuffix(int rank)
    {
        string suffix = "th";
        if (rank % 100 < 11 || rank % 100 > 13)
            switch (rank % 10)
            {
                case 1: suffix = "st"; break;
                case 2: suffix = "nd"; break;
                case 3: suffix = "rd"; break;
            }
        return $"{rank}{suffix}";
    }

    public void RefreshRacers()
    {
        if (IsServerStarted) InitializeRacers();
    }

    public int GetLocalPlayerRank()
        => latestSnapshot.FirstOrDefault(s => s.isLocalPlayer).rank;

    public List<RankSnapshot> GetFinalRankings()
        => latestSnapshot.ToList();

    public string GetRacerName(int rank)
        => latestSnapshot.FirstOrDefault(s => s.rank == rank).name ?? "Unknown";

    public bool IsLocalPlayerRank(int rank)
        => latestSnapshot.FirstOrDefault(s => s.rank == rank).isLocalPlayer;
}