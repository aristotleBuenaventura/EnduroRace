using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using Firebase.Firestore;
using Firebase.Extensions;

public class EdgegapRelayManager : MonoBehaviour
{
    public static EdgegapRelayManager Instance;

    [Header("Edgegap Settings")]
    public string apiToken = "00e9a28b-d9a9-4ca1-b6fb-6cbd6e98cff3";
    public string relayProfileName = "Endurorace";

    private const string BASE_URL = "https://api.edgegap.com/v1/relays";

    private string sessionId;
    private string relayHost;
    private ushort relayServerPort;
    private ushort relayClientPort;
    private uint sessionAuthToken;
    private uint userAuthToken;

    private FirebaseFirestore db;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void CreateRelaySession(string lobbyId, int playerCount, System.Action<bool> onComplete)
    {
        StartCoroutine(CreateSessionCoroutine(lobbyId, playerCount, onComplete));
    }

    private IEnumerator CreateSessionCoroutine(string lobbyId, int playerCount, System.Action<bool> onComplete)
    {
        Debug.Log("[EdgegapRelay] Creating relay session...");

        // Get public IP
        string publicIP = "";
        using (UnityWebRequest ipRequest = UnityWebRequest.Get("https://api.ipify.org"))
        {
            yield return ipRequest.SendWebRequest();
            if (ipRequest.result == UnityWebRequest.Result.Success)
                publicIP = ipRequest.downloadHandler.text.Trim();
            else
                Debug.LogWarning("[EdgegapRelay] Could not get public IP: " + ipRequest.error);
        }

        Debug.Log($"[EdgegapRelay] Public IP: {publicIP}");

        // Build users array
        var usersSb = new StringBuilder("[");
        for (int i = 0; i < playerCount; i++)
        {
            usersSb.Append($"{{\"ip\":\"{publicIP}\"}}");
            if (i < playerCount - 1)
                usersSb.Append(",");
        }
        usersSb.Append("]");

        string jsonBody = $"{{\"relay_profile_id\":\"{relayProfileName}\",\"session_identifier\":\"{lobbyId}\",\"users\":{usersSb}}}";

        Debug.Log($"[EdgegapRelay] Request body: {jsonBody}");

        // Create session
        using (UnityWebRequest request = new UnityWebRequest(BASE_URL + "/sessions", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "token " + apiToken);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[EdgegapRelay] Failed: {request.error}");
                Debug.LogError($"[EdgegapRelay] Response code: {request.responseCode}");
                Debug.LogError($"[EdgegapRelay] Response body: {request.downloadHandler.text}");
                onComplete?.Invoke(false);
                yield break;
            }

            string responseText = request.downloadHandler.text;
            Debug.Log($"[EdgegapRelay] Session created: {responseText}");

            sessionId = ExtractJsonField(responseText, "session_id");
            Debug.Log($"[EdgegapRelay] Session ID: {sessionId}");

            if (string.IsNullOrEmpty(sessionId))
            {
                Debug.LogError("[EdgegapRelay] Could not parse session_id!");
                onComplete?.Invoke(false);
                yield break;
            }
        }

        // Poll until relay is ready
        Debug.Log("[EdgegapRelay] Waiting for relay to become ready...");
        float timeout = 30f;
        float elapsed = 0f;
        bool relayReady = false;

        while (elapsed < timeout)
        {
            yield return new WaitForSeconds(2f);
            elapsed += 2f;

            using (UnityWebRequest pollRequest = UnityWebRequest.Get(BASE_URL + "/sessions/" + sessionId))
            {
                pollRequest.SetRequestHeader("Authorization", "token " + apiToken);
                yield return pollRequest.SendWebRequest();

                if (pollRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning($"[EdgegapRelay] Poll failed: {pollRequest.error}");
                    continue;
                }

                string pollResponse = pollRequest.downloadHandler.text;
                Debug.Log($"[EdgegapRelay] Poll response: {pollResponse}");

                string status = ExtractJsonField(pollResponse, "status");
                string readyStr = ExtractJsonField(pollResponse, "ready");

                Debug.Log($"[EdgegapRelay] Status: {status}, Ready: {readyStr}");

                if (readyStr == "true" || status == "Linked" || status == "Ready")
                {
                    // Parse session auth token
                    string sessionTokenStr = ExtractJsonField(pollResponse, "authorization_token");
                    uint.TryParse(sessionTokenStr, out sessionAuthToken);

                    // Parse user auth token from session_users array
                    int usersStart = pollResponse.IndexOf("\"session_users\":");
                    if (usersStart != -1)
                    {
                        string usersSection = pollResponse.Substring(usersStart);
                        string userTokenStr = ExtractJsonField(usersSection, "authorization_token");
                        uint.TryParse(userTokenStr, out userAuthToken);
                    }

                    Debug.Log($"[EdgegapRelay] Auth tokens - Session: {sessionAuthToken}, User: {userAuthToken}");

                    // Find relay object
                    int relayObjStart = pollResponse.IndexOf("\"relay\": {");
                    if (relayObjStart == -1)
                        relayObjStart = pollResponse.IndexOf("\"relay\":{");

                    if (relayObjStart != -1)
                    {
                        string relayObj = pollResponse.Substring(relayObjStart);

                        // Get host
                        relayHost = ExtractJsonField(relayObj, "host");
                        if (string.IsNullOrEmpty(relayHost))
                            relayHost = ExtractJsonField(relayObj, "ip");

                        // Get client port
                        int clientPortStart = relayObj.IndexOf("\"client\": {");
                        if (clientPortStart == -1)
                            clientPortStart = relayObj.IndexOf("\"client\":{");

                        if (clientPortStart != -1)
                        {
                            string clientObj = relayObj.Substring(clientPortStart);
                            string portStr = ExtractJsonField(clientObj, "port");
                            if (!ushort.TryParse(portStr, out relayClientPort))
                                relayClientPort = 7770;
                        }

                        // Get server port
                        int serverPortStart = relayObj.IndexOf("\"server\": {");
                        if (serverPortStart == -1)
                            serverPortStart = relayObj.IndexOf("\"server\":{");

                        if (serverPortStart != -1)
                        {
                            string serverObj = relayObj.Substring(serverPortStart);
                            string serverPortStr = ExtractJsonField(serverObj, "port");
                            if (!ushort.TryParse(serverPortStr, out relayServerPort))
                                relayServerPort = 7770;
                        }

                        Debug.Log($"[EdgegapRelay] Relay ready! Host: {relayHost}, ServerPort: {relayServerPort}, ClientPort: {relayClientPort}");
                        relayReady = true;
                        break;
                    }
                    else
                    {
                        Debug.LogWarning($"[EdgegapRelay] Ready but relay object not found");
                    }
                }
            }
        }

        if (!relayReady || string.IsNullOrEmpty(relayHost))
        {
            Debug.LogError("[EdgegapRelay] Relay never became ready!");
            onComplete?.Invoke(false);
            yield break;
        }

        yield return WriteRelayToFirebase(lobbyId);
        onComplete?.Invoke(true);
    }

    private IEnumerator WriteRelayToFirebase(string lobbyId)
    {
        bool done = false;

        db.Collection("lobbies").Document(lobbyId)
            .UpdateAsync(new Dictionary<string, object>
            {
                { "relayHost", relayHost },
                { "relayClientPort", (int)relayClientPort },
                { "relayServerPort", (int)relayServerPort },
                { "relaySessionToken", (long)sessionAuthToken },
                { "relayUserToken", (long)userAuthToken },
                { "relaySessionId", sessionId }
            })
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                    Debug.LogError("[EdgegapRelay] Failed to write to Firebase: " + task.Exception);
                else
                    Debug.Log($"[EdgegapRelay] Written to Firebase: {relayHost} server:{relayServerPort} client:{relayClientPort} sessionToken:{sessionAuthToken} userToken:{userAuthToken}");
                done = true;
            });

        yield return new WaitUntil(() => done);
    }

    public void DeleteRelaySession()
    {
        if (string.IsNullOrEmpty(sessionId)) return;
        StartCoroutine(DeleteSessionCoroutine());
    }

    private IEnumerator DeleteSessionCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Delete(BASE_URL + "/sessions/" + sessionId))
        {
            request.SetRequestHeader("Authorization", "token " + apiToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("[EdgegapRelay] Session deleted");
            else
                Debug.LogWarning("[EdgegapRelay] Failed to delete session: " + request.error);
        }

        sessionId = null;
    }

    private string ExtractJsonField(string json, string fieldName)
    {
        string search = $"\"{fieldName}\":";
        int start = json.IndexOf(search);
        if (start == -1)
        {
            search = $"\"{fieldName}\": ";
            start = json.IndexOf(search);
        }
        if (start == -1) return "";

        start += search.Length;

        while (start < json.Length && json[start] == ' ') start++;

        if (start >= json.Length) return "";

        if (json[start] == '"')
        {
            start++;
            int end = json.IndexOf('"', start);
            return end == -1 ? "" : json.Substring(start, end - start);
        }
        else
        {
            int end = start;
            while (end < json.Length && json[end] != ',' && json[end] != '}' && json[end] != ']')
                end++;
            return json.Substring(start, end - start).Trim();
        }
    }
}