using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpeedLinesEffect : MonoBehaviour
{
    public static SpeedLinesEffect Instance { get; private set; }

    [Header("Speed Lines Settings")]
    [SerializeField] private int lineCount = 40;
    [SerializeField] private float minLength = 100f;
    [SerializeField] private float maxLength = 300f;
    [SerializeField] private float lineWidth = 1.5f;
    [SerializeField] private float innerRadius = 150f;
    [SerializeField] private float outerRadius = 600f;

    [Header("Animation")]
    [SerializeField] private float fadeInSpeed = 5f;
    [SerializeField] private float fadeOutSpeed = 4f;
    [SerializeField] private float maxAlpha = 0.6f;
    [SerializeField] private float moveSpeed = 800f;

    [Header("Color")]
    [SerializeField] private Color lineColor = new Color(1f, 1f, 1f, 1f);

    private Canvas canvas;
    private List<RectTransform> lines = new List<RectTransform>();
    private List<float> lineAngles = new List<float>();
    private float currentAlpha = 0f;
    private bool isSprinting = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        SetupCanvas();
        CreateLines();
    }

    private void SetupCanvas()
    {
        canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 99;

        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
    }

    private void CreateLines()
    {
        for (int i = 0; i < lineCount; i++)
        {
            float angle = Random.Range(0f, 360f);
            lineAngles.Add(angle);

            GameObject lineObj = new GameObject($"SpeedLine_{i}");
            lineObj.transform.SetParent(transform, false);

            RectTransform rt = lineObj.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(lineWidth, Random.Range(minLength, maxLength));

            Image img = lineObj.AddComponent<Image>();
            img.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0f);

            // Position line from center outward
            Vector2 dir = AngleToDirection(angle);
            float dist = Random.Range(innerRadius, outerRadius);
            rt.anchoredPosition = dir * dist;
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0f);
            rt.localRotation = Quaternion.Euler(0f, 0f, -angle);

            lines.Add(rt);
        }
    }

    private void Update()
    {
        // Fade in/out
        float targetAlpha = isSprinting ? maxAlpha : 0f;
        currentAlpha = Mathf.MoveTowards(
            currentAlpha,
            targetAlpha,
            (isSprinting ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime);

        // Move and update lines
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i] == null) continue;

            // Move outward
            Vector2 dir = AngleToDirection(lineAngles[i]);
            lines[i].anchoredPosition += dir * moveSpeed * Time.deltaTime;

            // Reset when out of bounds
            float dist = lines[i].anchoredPosition.magnitude;
            if (dist > outerRadius)
            {
                lineAngles[i] = Random.Range(0f, 360f);
                dir = AngleToDirection(lineAngles[i]);
                lines[i].anchoredPosition = dir * innerRadius;
                lines[i].sizeDelta = new Vector2(lineWidth, Random.Range(minLength, maxLength));
                lines[i].localRotation = Quaternion.Euler(0f, 0f, -lineAngles[i]);
            }

            // Apply alpha
            Image img = lines[i].GetComponent<Image>();
            if (img != null)
            {
                // Lines closer to center are more transparent
                float alphaFactor = Mathf.Clamp01((dist - innerRadius) / (outerRadius - innerRadius));
                img.color = new Color(lineColor.r, lineColor.g, lineColor.b,
                                      currentAlpha * alphaFactor);
            }
        }
    }

    private Vector2 AngleToDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
    }

    public void SetSprinting(bool sprinting) => isSprinting = sprinting;
}