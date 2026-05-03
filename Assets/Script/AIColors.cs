using System.Collections.Generic;
using UnityEngine;

public class AIColors : MonoBehaviour
{
    [Header("Color Targets (3 GameObjects)")]
    [SerializeField] private GameObject colorTarget1;
    [SerializeField] private GameObject colorTarget2;
    [SerializeField] private GameObject colorTarget3;

    [Header("Color Settings")]
    [SerializeField] private bool includeChildrenRenderers = true;

    private static readonly Color[] ColorPalette = new Color[]
    {
        new Color32(58, 46, 73, 255),   // dark violet
        new Color32(42, 66, 98, 255),   // steel blue
        new Color32(36, 78, 82, 255),   // deep teal
        new Color32(63, 73, 46, 255),   // olive
        new Color32(74, 49, 40, 255),   // chestnut
        new Color32(49, 55, 66, 255),   // slate
        new Color32(72, 57, 92, 255),   // muted purple
        new Color32(39, 71, 64, 255),   // pine
        new Color32(84, 64, 38, 255),   // bronze
        new Color32(63, 42, 52, 255),   // wine
        new Color32(55, 68, 39, 255),   // moss
        new Color32(44, 49, 78, 255),   // indigo slate
        new Color32(88, 70, 56, 255),   // clay
        new Color32(46, 58, 48, 255),   // forest gray
        new Color32(70, 53, 67, 255),   // dusty plum
        new Color32(36, 63, 74, 255),   // ocean dusk
        new Color32(77, 60, 35, 255),   // dark ochre
        new Color32(57, 43, 41, 255),   // cocoa
        new Color32(41, 54, 60, 255),   // blue gray
        new Color32(66, 61, 43, 255),   // faded olive
    };

    private static readonly HashSet<int> UsedColorIndices = new HashSet<int>();
    private static readonly Dictionary<string, int> AssignedIndicesByKey = new Dictionary<string, int>();

    private int assignedColorIndex = -1;
    private string assignmentKey;

    private void OnEnable()
    {
        AssignAndApplyColor();
    }

    private void OnDisable()
    {
        ReleaseAssignedColor();
    }

    private void AssignAndApplyColor()
    {
        assignmentKey = GetAssignmentKey();

        if (string.IsNullOrEmpty(assignmentKey))
            assignmentKey = $"AI_{GetInstanceID()}";

        if (!AssignedIndicesByKey.TryGetValue(assignmentKey, out assignedColorIndex))
        {
            assignedColorIndex = GetNextAvailableColorIndex();
            AssignedIndicesByKey[assignmentKey] = assignedColorIndex;
        }

        UsedColorIndices.Add(assignedColorIndex);
        ApplyColorToTargets(ColorPalette[assignedColorIndex]);
    }

    private void ReleaseAssignedColor()
    {
        if (string.IsNullOrEmpty(assignmentKey))
            return;

        if (AssignedIndicesByKey.TryGetValue(assignmentKey, out int idx) && idx == assignedColorIndex)
        {
            AssignedIndicesByKey.Remove(assignmentKey);
            UsedColorIndices.Remove(assignedColorIndex);
        }
    }

    private string GetAssignmentKey()
    {
        NetworkedAIOpponent ai = GetComponent<NetworkedAIOpponent>();
        if (ai == null)
            ai = GetComponentInParent<NetworkedAIOpponent>();

        if (ai != null && !string.IsNullOrWhiteSpace(ai.opponentName))
            return ai.opponentName.Trim();

        return gameObject.name;
    }

    private static int GetNextAvailableColorIndex()
    {
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            if (!UsedColorIndices.Contains(i))
                return i;
        }

        // Fallback: kung lampas 20 active AI, repeat from start.
        return 0;
    }

    private void ApplyColorToTargets(Color colorValue)
    {
        ApplyColorToTarget(colorTarget1, colorValue);
        ApplyColorToTarget(colorTarget2, colorValue);
        ApplyColorToTarget(colorTarget3, colorValue);
    }

    private void ApplyColorToTarget(GameObject target, Color colorValue)
    {
        if (target == null)
            return;

        Renderer[] renderers = includeChildrenRenderers
            ? target.GetComponentsInChildren<Renderer>(true)
            : target.GetComponents<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer currentRenderer = renderers[i];
            if (currentRenderer == null)
                continue;

            Material[] mats = currentRenderer.materials;
            for (int j = 0; j < mats.Length; j++)
            {
                Material mat = mats[j];
                if (mat == null)
                    continue;

                if (mat.HasProperty("_BaseColor"))
                    mat.SetColor("_BaseColor", colorValue);
                else if (mat.HasProperty("_Color"))
                    mat.color = colorValue;
            }
        }
    }
}
