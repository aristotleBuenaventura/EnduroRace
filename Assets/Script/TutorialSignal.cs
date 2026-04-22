using System;
using UnityEngine;

public static class TutorialSignal
{
    public static event Action<int> OnTutorialReady;
    public static int PendingStep = -1;

    public static void Dispatch(int step)
    {
        Debug.Log($"[TutorialSignal] Dispatched step={step}");
        OnTutorialReady?.Invoke(step);
    }

    public static void Clear()
    {
        PendingStep = -1;
    }
}