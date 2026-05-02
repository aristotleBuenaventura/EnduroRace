using UnityEngine;
using Firebase.Firestore;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Extensions;

public class LobbyCountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int countdownDuration = 10;

    private FirebaseFirestore db;
    private string lobbyId;
    private string localPlayerId;

    private ListenerRegistration lobbyListener;
    private bool countdownRunning;
    private bool isLoadingScene;

    void Start()
    {
        StartCoroutine(InitWhenLobbyReady());
    }

    private IEnumerator InitWhenLobbyReady()
    {
        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
        yield return new WaitUntil(() => lobby.IsLobbyReady);

        lobbyId = lobby.GetLobbyId();
        localPlayerId = lobby.GetPlayerId();

        ListenToLobby();
    }

    private void ListenToLobby()
    {
        lobbyListener = db.Collection("lobbies")
            .Document(lobbyId)
            .Listen(snapshot =>
            {
                if (!snapshot.Exists) return;

                string state = snapshot.ContainsField("state")
                    ? snapshot.GetValue<string>("state")
                    : "waiting";

                if (state == "waiting")
                {
                    countdownRunning = false;
                    countdownText.text = "Waiting for players to ready up...";
                    return;
                }

                if (state == "countdown" && snapshot.ContainsField("countdownStartTime"))
                {
                    Timestamp startTimestamp = snapshot.GetValue<Timestamp>("countdownStartTime");

                    if (snapshot.ContainsField("countdownDuration"))
                        countdownDuration = (int)snapshot.GetValue<long>("countdownDuration");

                    StartCountdown(startTimestamp.ToDateTime());
                }

                if (state == "starting" && !isLoadingScene)
                {
                    countdownText.text = "Loading race...";
                    isLoadingScene = true;
                    StartCoroutine(CollectPlayersAndLoadScene());
                }
            });
    }

    private void StartCountdown(DateTime startTime)
    {
        if (countdownRunning) return;
        countdownRunning = true;
        StartCoroutine(CountdownCoroutine(startTime));
    }

    private IEnumerator CountdownCoroutine(DateTime startTime)
    {
        while (countdownRunning)
        {
            double remaining = countdownDuration - (DateTime.UtcNow - startTime).TotalSeconds;
            if (remaining <= 0)
            {
                countdownText.text = "Starting race...";
                countdownRunning = false;

                LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
                ;

                if (lobby.IsHost)
                {
                    ;

                    var playersTask = db.Collection("lobbies")
                        .Document(lobbyId)
                        .Collection("players")
                        .GetSnapshotAsync();

                    yield return new WaitUntil(() => playersTask.IsCompleted);

                    int playerCount = playersTask.IsFaulted
                        ? 8
                        : playersTask.Result.Documents.Count();

                    ;

                    bool relayCreated = false;
                    bool relayDone = false;

                    if (EdgegapRelayManager.Instance != null)
                    {
                        ;

                        EdgegapRelayManager.Instance.CreateRelaySession(
                            lobbyId,
                            playerCount,
                            success =>
                            {
                                relayCreated = success;
                                relayDone = true;
                                ;
                            }
                        );

                        float elapsed = 0f;
                        while (!relayDone && elapsed < 35f)
                        {
                            elapsed += Time.deltaTime;
                            yield return null;
                        }

                        if (!relayCreated)
                            ;
                    }
                    else
                    {
                        ;
                    }

                    // Wait before telling clients to load so host has time to start server
                    yield return new WaitForSeconds(1f);

                    // Set state to starting so all clients load scene
                    db.Collection("lobbies").Document(lobbyId)
                        .UpdateAsync("state", "starting");
                }

                yield break;
            }

            countdownText.text = $"Race starts in {Mathf.CeilToInt((float)remaining)}";
            yield return null;
        }
    }

    private IEnumerator CollectPlayersAndLoadScene()
    {
        ;

        // Get lobby document for relay data
        var lobbyTask = db.Collection("lobbies")
            .Document(lobbyId)
            .GetSnapshotAsync();

        yield return new WaitUntil(() => lobbyTask.IsCompleted);

        string relayHost = "";
        ushort relayServerPort = 7770;
        ushort relayClientPort = 7770;
        uint relaySessionToken = 0;
        uint relayUserToken = 0;

        if (!lobbyTask.IsFaulted && lobbyTask.Result.Exists)
        {
            relayHost = lobbyTask.Result.ContainsField("relayHost")
                ? lobbyTask.Result.GetValue<string>("relayHost") : "";

            relayServerPort = lobbyTask.Result.ContainsField("relayServerPort")
                ? (ushort)lobbyTask.Result.GetValue<long>("relayServerPort")
                : (ushort)7770;

            relayClientPort = lobbyTask.Result.ContainsField("relayClientPort")
                ? (ushort)lobbyTask.Result.GetValue<long>("relayClientPort")
                : (ushort)7770;

            relaySessionToken = lobbyTask.Result.ContainsField("relaySessionToken")
                ? (uint)lobbyTask.Result.GetValue<long>("relaySessionToken")
                : 0u;

            relayUserToken = lobbyTask.Result.ContainsField("relayUserToken")
                ? (uint)lobbyTask.Result.GetValue<long>("relayUserToken")
                : 0u;

            bool isHost = FindFirstObjectByType<LobbyManager>()?.IsHost ?? false;
            ;
        }

        // Get all players
        var playersTask = db.Collection("lobbies")
            .Document(lobbyId)
            .Collection("players")
            .GetSnapshotAsync();

        yield return new WaitUntil(() => playersTask.IsCompleted);

        if (playersTask.IsFaulted)
        {
            ;
            yield break;
        }

        List<LobbyDataTransfer.PlayerData> playerDataList = new List<LobbyDataTransfer.PlayerData>();

        foreach (var doc in playersTask.Result.Documents)
        {
            var playerData = new LobbyDataTransfer.PlayerData
            {
                playerId    = doc.Id,
                displayName = doc.ContainsField("name")
                    ? doc.GetValue<string>("name") : "Player",
                avatar      = doc.ContainsField("avatar")
                    ? doc.GetValue<string>("avatar") : "DefaultAvatar",
                tier        = doc.ContainsField("tier")
                    ? doc.GetValue<string>("tier") : "Beginner",
                selectedModel = doc.ContainsField("selectedModel")   // ← NEW
                    ? doc.GetValue<string>("selectedModel") : "Male",
                isBot       = doc.ContainsField("isBot") && doc.GetValue<bool>("isBot"),
                isLocalPlayer = doc.Id == localPlayerId
            };

            playerDataList.Add(playerData);
            ;
        }

        if (LobbyDataTransfer.Instance == null)
        {
            GameObject transferObj = new GameObject("LobbyDataTransfer");
            transferObj.AddComponent<LobbyDataTransfer>();
        }

        bool amHost = FindFirstObjectByType<LobbyManager>()?.IsHost ?? false;

        LobbyDataTransfer.Instance.SetLobbyData(lobbyId, localPlayerId, playerDataList);
        LobbyDataTransfer.Instance.isLocalPlayerHost = amHost;
        LobbyDataTransfer.Instance.relayHost         = relayHost;
        LobbyDataTransfer.Instance.relayServerPort   = relayServerPort;
        LobbyDataTransfer.Instance.relayClientPort   = relayClientPort;
        LobbyDataTransfer.Instance.relaySessionToken = relaySessionToken;
        LobbyDataTransfer.Instance.relayUserToken    = relayUserToken;

        // Clients wait extra time for host to start server through relay
        if (!amHost)
        {
            ;
            yield return new WaitForSeconds(5f);
        }

        lobbyListener?.Stop();

        ;
        string tier = LobbyDataTransfer.Instance?.GetLocalPlayerData()?.tier ?? "Beginner";
        string sceneName = tier switch
        {
            "Intermediate" => "RaceScene_Intermediate",
            "Pro"          => "RaceScene_Pro",
            _              => "RaceScene"
        };
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        lobbyListener?.Stop();
    }
}