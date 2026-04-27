using UnityEngine;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using System.Linq;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    private FirebaseFirestore db;
    private string lobbyId;
    private string playerId;
    private string playerTier;

    public bool IsLobbyReady { get; private set; }
    public bool IsHost { get; private set; }

    public event Action OnLobbyReady;

    private ListenerRegistration readyListener;

    private const int MAX_PLAYERS = 20;
    private const int MIN_PLAYERS = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(InitWhenFirebaseReady());
    }

    private IEnumerator InitWhenFirebaseReady()
    {
        yield return new WaitUntil(() => FirebaseManager.Instance != null);
        yield return new WaitUntil(() => FirebaseManager.Instance.IsFirebaseReady);

        Init();
    }

    private void Init()
    {
        db = FirebaseManager.Instance.Db;
        playerId = FirebaseManager.Instance.PlayerId;

        Debug.Log($"[LobbyManager] ═══════════════════════════════════════");
        Debug.Log($"[LobbyManager] Initializing");
        Debug.Log($"[LobbyManager] PlayerId: {playerId}");
        Debug.Log($"[LobbyManager] Platform: {Application.platform}");
        Debug.Log($"[LobbyManager] ═══════════════════════════════════════");

        db.Collection("players").Document(playerId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                playerTier = "Beginner";

                if (!task.IsFaulted && task.Result.Exists && task.Result.ContainsField("tier"))
                    playerTier = task.Result.GetValue<string>("tier");

                Debug.Log($"[LobbyManager] Player tier: {playerTier}");
                JoinOrCreateLobby(playerTier);
            });
    }

    private void JoinOrCreateLobby(string tier)
    {
        Debug.Log($"[LobbyManager] Searching for lobby with tier='{tier}', state='waiting', currentPlayers < {MAX_PLAYERS}");
        
        db.Collection("lobbies")
            .WhereEqualTo("tier", tier)
            .WhereEqualTo("state", "waiting")
            .WhereLessThan("currentPlayers", MAX_PLAYERS)
            .GetSnapshotAsync() // ✅ Get ALL matching lobbies first
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError($"[LobbyManager] Query failed: {task.Exception}");
                    CreateLobby(tier);
                    return;
                }

                var lobbies = task.Result.Documents.ToList();
                Debug.Log($"[LobbyManager] Found {lobbies.Count} matching lobbies");

                if (lobbies.Count == 0)
                {
                    Debug.Log("[LobbyManager] No available lobbies - Creating new one");
                    CreateLobby(tier);
                    return;
                }

                // ✅ Pick the lobby with the most players (helps consolidate)
                var bestLobby = lobbies.OrderByDescending(l => l.GetValue<long>("currentPlayers")).First();
                lobbyId = bestLobby.Id;
                
                Debug.Log($"[LobbyManager] ✅ Joining lobby: {lobbyId}");
                Debug.Log($"[LobbyManager]    Current players: {bestLobby.GetValue<long>("currentPlayers")}");
                Debug.Log($"[LobbyManager]    Host: {bestLobby.GetValue<string>("hostId")}");
                
                AddPlayerToLobby();
            });
    }

    private void CreateLobby(string tier)
    {
        lobbyId = Guid.NewGuid().ToString();
        Debug.Log($"[LobbyManager] Creating NEW lobby: {lobbyId}");

        var data = new Dictionary<string, object>
        {
            { "maxPlayers", MAX_PLAYERS },
            { "currentPlayers", 0 },
            { "tier", tier },
            { "createdAt", Timestamp.GetCurrentTimestamp() },
            { "hostId", playerId },
            { "state", "waiting" }
        };

        db.Collection("lobbies").Document(lobbyId)
            .SetAsync(data)
            .ContinueWithOnMainThread(_ =>
            {
                Debug.Log($"[LobbyManager] Lobby created successfully");
                AddPlayerToLobby();
            });
    }

    private void AddPlayerToLobby()
    {
        Debug.Log($"[LobbyManager] Adding player {playerId} to lobby {lobbyId}");

        db.Collection("players")
        .Document(playerId)
        .GetSnapshotAsync()
        .ContinueWithOnMainThread(profileTask =>
        {
            if (profileTask.IsFaulted || !profileTask.Result.Exists)
            {
                Debug.LogError("[LobbyManager] Player profile not found!");
                return;
            }

            var profile = profileTask.Result;

            string displayName = profile.ContainsField("displayName")
                ? profile.GetValue<string>("displayName")
                : "Player";

            string avatar = profile.ContainsField("avatar")
                ? profile.GetValue<string>("avatar")
                : "DefaultAvatar";

            string tier = profile.ContainsField("tier")
                ? profile.GetValue<string>("tier")
                : "Beginner";

            string selectedModel = profile.ContainsField("selectedModel")
                ? profile.GetValue<string>("selectedModel") 
                : "Male";

            Debug.Log($"[LobbyManager] Player info - Name: {displayName}, Avatar: {avatar}, Tier: {tier}");

            var playerRef = db.Collection("lobbies")
                .Document(lobbyId)
                .Collection("players")
                .Document(playerId);

            var playerData = new Dictionary<string, object>
            {
                { "isBot",          false         },
                { "isReady",        false         },
                { "name",           displayName   },
                { "avatar",         avatar        },
                { "tier",           tier          },
                { "selectedModel",  selectedModel }, // ── ADD THIS ──
                { "joinedAt",       Timestamp.GetCurrentTimestamp() },
                { "lastHeartbeat",  Timestamp.GetCurrentTimestamp() }
            };

            playerRef.SetAsync(playerData)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError("[LobbyManager] Failed to add player: " + task.Exception);
                        return;
                    }

                    Debug.Log("[LobbyManager] ✅ Successfully added to lobby");

                    var lobbyRef = db.Collection("lobbies").Document(lobbyId);
                    lobbyRef.UpdateAsync("currentPlayers", FieldValue.Increment(1));

                    DetermineHost();
                    StartCoroutine(HeartbeatCoroutine());
                });
        });
    }

    private IEnumerator HeartbeatCoroutine()
    {
        while (!string.IsNullOrEmpty(lobbyId))
        {
            yield return new WaitForSeconds(5f);
            
            if (string.IsNullOrEmpty(lobbyId)) break;
            
            db.Collection("lobbies")
                .Document(lobbyId)
                .Collection("players")
                .Document(playerId)
                .UpdateAsync("lastHeartbeat", Timestamp.GetCurrentTimestamp())
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogWarning("[LobbyManager] Heartbeat failed");
                    }
                });
        }
    }

    private void DetermineHost()
    {
        db.Collection("lobbies").Document(lobbyId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || !task.Result.Exists)
                {
                    Debug.LogError("[LobbyManager] Failed to determine host");
                    return;
                }

                string hostId = task.Result.GetValue<string>("hostId");
                IsHost = hostId == playerId;

                Debug.Log($"[LobbyManager] Host check - HostId: {hostId}, IsHost: {IsHost}");

                if (IsHost)
                {
                    Debug.Log("[LobbyManager] 👑 I AM THE HOST");
                    StartReadyListener();
                    StartCoroutine(CleanupGhostPlayers());
                }
                else
                {
                    Debug.Log("[LobbyManager] I am NOT the host - checking if host exists...");
                    CheckAndClaimHostIfNeeded();  // ← Add this!
                }

                IsLobbyReady = true;
                OnLobbyReady?.Invoke();
                
                Debug.Log($"[LobbyManager] ✅ Lobby ready! LobbyId: {lobbyId}");
            });
    }

    private IEnumerator CleanupGhostPlayers()
    {
        while (IsHost && !string.IsNullOrEmpty(lobbyId))
        {
            yield return new WaitForSeconds(10f);
            
            if (!IsHost || string.IsNullOrEmpty(lobbyId)) break;
            
            db.Collection("lobbies")
                .Document(lobbyId)
                .Collection("players")
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted) return;
                    
                    DateTime now = DateTime.UtcNow;
                    
                    foreach (var doc in task.Result.Documents)
                    {
                        string playerName = doc.ContainsField("name") ? doc.GetValue<string>("name") : "Unknown";
                        bool isBot = doc.ContainsField("isBot") && doc.GetValue<bool>("isBot");
                        
                        if (isBot) continue;
                        
                        if (!doc.ContainsField("lastHeartbeat"))
                        {
                            Debug.LogWarning($"[LobbyManager] Player {playerName} has no heartbeat - OLD PLAYER");
                            continue;
                        }
                        
                        Timestamp lastHeartbeat = doc.GetValue<Timestamp>("lastHeartbeat");
                        DateTime heartbeatTime = lastHeartbeat.ToDateTime();
                        double secondsSinceHeartbeat = (now - heartbeatTime).TotalSeconds;
                        
                        Debug.Log($"[LobbyManager] Player {playerName} - Last heartbeat: {secondsSinceHeartbeat:F1}s ago");
                        
                        if (secondsSinceHeartbeat > 15)
                        {
                            Debug.Log($"[LobbyManager] 🗑️ Removing ghost: {playerName}");
                            
                            doc.Reference.DeleteAsync()
                                .ContinueWithOnMainThread(_ =>
                                {
                                    var lobbyRef = db.Collection("lobbies").Document(lobbyId);
                                    lobbyRef.UpdateAsync(new Dictionary<string, object>
                                    {
                                        { "currentPlayers", FieldValue.Increment(-1) },
                                        { "state", "waiting" },
                                        { "countdownStartTime", FieldValue.Delete }
                                    });
                                });
                        }
                    }
                });
        }
    }

    private void StartReadyListener()
    {
        if (!IsHost) return;

        Debug.Log("[LobbyManager] Starting ready listener");

        readyListener?.Stop();

        readyListener = db.Collection("lobbies")
            .Document(lobbyId)
            .Collection("players")
            .Listen(async snapshot =>
            {
                Debug.Log("[LobbyManager] ═════════════════════════════════════════");
                Debug.Log("[LobbyManager] Ready check triggered");

                int humans = 0;
                int readyHumans = 0;

                foreach (var doc in snapshot.Documents)
                {
                    string name = doc.ContainsField("name") ? doc.GetValue<string>("name") : "Unknown";
                    bool isBot = doc.ContainsField("isBot") && doc.GetValue<bool>("isBot");
                    bool isReady = doc.ContainsField("isReady") && doc.GetValue<bool>("isReady");

                    Debug.Log($"[LobbyManager]   {name}: Bot={isBot}, Ready={isReady}");

                    if (!isBot)
                    {
                        humans++;
                        if (isReady) readyHumans++;
                    }
                }

                Debug.Log($"[LobbyManager] Humans: {humans}, Ready: {readyHumans}");

                var lobbyRef = db.Collection("lobbies").Document(lobbyId);
                var lobbySnap = await lobbyRef.GetSnapshotAsync(Source.Server);  // Force server read

                string state = lobbySnap.GetValue<string>("state");
                Debug.Log($"[LobbyManager] Current state: {state}");

                if (humans >= MIN_PLAYERS && humans == readyHumans && readyHumans > 0)
                {
                    if (state == "waiting")
                    {
                        Debug.Log("[LobbyManager] 🚀 STARTING COUNTDOWN!");
                        
                        await lobbyRef.UpdateAsync(new Dictionary<string, object>
                        {
                            { "state", "countdown" },
                            { "countdownStartTime", Timestamp.GetCurrentTimestamp() },
                            { "countdownDuration", 10 }
                        });
                    }
                }
                else
                {
                    if (state == "countdown")
                    {
                        Debug.Log("[LobbyManager] ❌ Canceling countdown");
                        
                        await lobbyRef.UpdateAsync(new Dictionary<string, object>
                        {
                            { "state", "waiting" },
                            { "countdownStartTime", FieldValue.Delete }
                        });
                    }
                }
                Debug.Log("[LobbyManager] ═════════════════════════════════════════");
            });
    }

    public void LeaveLobby()
    {
        if (string.IsNullOrEmpty(lobbyId)) return;

        Debug.Log($"[LobbyManager] Leaving lobby");

        StopAllCoroutines();

        var lobbyRef = db.Collection("lobbies").Document(lobbyId);

        db.Collection("lobbies")
            .Document(lobbyId)
            .Collection("players")
            .Document(playerId)
            .DeleteAsync()
            .ContinueWithOnMainThread(_ =>
            {
                lobbyRef.UpdateAsync(new Dictionary<string, object>
                {
                    { "currentPlayers", FieldValue.Increment(-1) },
                    { "state", "waiting" },
                    { "countdownStartTime", FieldValue.Delete }
                });
            });
        
        lobbyId = null;
    }

    private void CheckAndClaimHostIfNeeded()
    {
        db.Collection("lobbies").Document(lobbyId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(lobbyTask =>
            {
                if (lobbyTask.IsFaulted || !lobbyTask.Result.Exists) return;

                string currentHostId = lobbyTask.Result.GetValue<string>("hostId");
                
                // Check if current host still exists
                db.Collection("lobbies")
                    .Document(lobbyId)
                    .Collection("players")
                    .Document(currentHostId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(hostCheckTask =>
                    {
                        // If host doesn't exist, claim host role
                        if (hostCheckTask.IsFaulted || !hostCheckTask.Result.Exists)
                        {
                            Debug.Log("[LobbyManager] 👑 Host is gone! Claiming host role...");
                            
                            db.Collection("lobbies")
                                .Document(lobbyId)
                                .UpdateAsync("hostId", playerId)
                                .ContinueWithOnMainThread(_ =>
                                {
                                    IsHost = true;
                                    Debug.Log("[LobbyManager] ✅ Successfully claimed host!");
                                    StartReadyListener();
                                    StartCoroutine(CleanupGhostPlayers());
                                });
                        }
                    });
            });
    }
    private void OnApplicationQuit()
    {
        LeaveLobby();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            LeaveLobby();
        }
    }

    private void OnDestroy()
    {
        readyListener?.Stop();
        if (!string.IsNullOrEmpty(lobbyId))
        {
            LeaveLobby();
        }
    }

    public string GetLobbyId() => lobbyId;
    public string GetPlayerId() => playerId;
}