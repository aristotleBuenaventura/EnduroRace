using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public FirebaseAuth Auth { get; private set; }
    public FirebaseFirestore Db { get; private set; }
    public string PlayerId { get; private set; }

    public bool IsFirebaseReady { get; private set; } = false;
    public event Action OnFirebaseReady;

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
            return;
        }

        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result != DependencyStatus.Available)
            {
                Debug.LogError("Firebase dependencies not available: " + task.Result);
                return;
            }

            Auth = FirebaseAuth.DefaultInstance;
            Db = FirebaseFirestore.DefaultInstance;

            SignInAnonymously();
        });
    }

    private void SignInAnonymously()
    {
        if (Auth.CurrentUser != null)
        {
            PlayerId = Auth.CurrentUser.UserId;
            Ready();
            return;
        }

        Auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Anonymous sign-in failed");
                return;
            }

            PlayerId = task.Result.User.UserId;
            Ready();
        });
    }

    private void Ready()
    {
        IsFirebaseReady = true;
        OnFirebaseReady?.Invoke();
        Debug.Log("Firebase is ready! PlayerId: " + PlayerId);
    }
}
