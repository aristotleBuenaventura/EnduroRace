using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseTester : MonoBehaviour
{
    void Start()
    {
        // Check Firebase dependencies
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var status = task.Result;
            if (status == DependencyStatus.Available)
            {
                Debug.Log("✅ Firebase is ready!");
            }
            else
            {
                Debug.LogError("❌ Firebase dependencies not available: " + status);
            }
        });
    }
}
