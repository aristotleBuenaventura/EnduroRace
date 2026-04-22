using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceProgressRing : MonoBehaviour
{
    [Header("References")]
    public Image ringImage;
    public MultiplayerRaceRanking rankingSystem;

    [Header("Optional Label")]
    public TextMeshProUGUI percentageText;

    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 2f;

    [Header("Colors")]
    [SerializeField] private Color startColor = new Color(0.3f, 0.8f, 1f);
    [SerializeField] private Color endColor   = new Color(1f, 0.8f, 0f);

    private float targetFill  = 0f;
    private float currentFill = 0f;
    private int   totalCheckpoints = 0;

    private void Awake()
    {
        if (ringImage == null)
            ringImage = GetComponent<Image>();
    }

    private void Start()
    {
        EnforceImageSettings();
    }

    private void EnforceImageSettings()
    {
        if (ringImage == null) return;

        ringImage.type          = Image.Type.Filled;
        ringImage.fillMethod    = Image.FillMethod.Radial360;
        ringImage.fillOrigin    = (int)Image.Origin360.Top;
        ringImage.fillClockwise = true;
    }

    private bool _waitingForData = true;

    private void Update()
    {
        if (rankingSystem == null) return;

        // Re-enforce every frame — Unity can silently reset Image.Type when
        // the sprite changes (e.g. during an atlas rebuild at runtime).
        EnforceImageSettings();

        if (rankingSystem.checkpoints != null && rankingSystem.checkpoints.Length > 0)
            totalCheckpoints = rankingSystem.checkpoints.Length;

        if (totalCheckpoints == 0) return;

        var rankings = rankingSystem.GetFinalRankings();

        if (rankings == null || rankings.Count == 0)
        {
            if (percentageText != null) percentageText.text = "...";
            return;
        }

        MultiplayerRaceRanking.RankSnapshot localSnap = default;
        bool found = false;

        foreach (var snap in rankings)
        {
            if (snap.isLocalPlayer)
            {
                localSnap = snap;
                found     = true;
                _waitingForData = false;
                break;
            }
        }

        if (!found)
        {
            if (percentageText != null) percentageText.text = "...";
            return;
        }

        // BUG FIX 3: The maximum reachable totalProgress is
        //   (totalCheckpoints * 1000f) + 999f
        // because each segment contributes up to 999 units of sub-progress.
        // Using only (totalCheckpoints * 1000f) as the divisor means the ring
        // can never reach 100 % during the final segment and jumps at
        // checkpoint crossings.  Use the full range instead.
        float maxProgress = totalCheckpoints * 1000f + 999f;
        if (maxProgress <= 0f) return;

        targetFill  = Mathf.Clamp01(localSnap.totalProgress / maxProgress);
        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * smoothSpeed);

        if (ringImage != null)
        {
            ringImage.fillAmount = currentFill;
            ringImage.color      = Color.Lerp(startColor, endColor, currentFill);
        }

        if (percentageText != null)
            percentageText.text = $"{Mathf.RoundToInt(currentFill * 100f)}%";
    }
}