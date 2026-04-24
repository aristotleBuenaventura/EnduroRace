using UnityEngine;

public class WaterWaveController : MonoBehaviour
{
    public Material waterMaterial;

    [Header("Wave Settings")]
    public float waveSpeed = 1f;
    public float waveStrength = 0.2f;
    public float waveScale = 1f;

    private float time;

    void Update()
    {
        time += Time.deltaTime * waveSpeed;

        // animate strength (parang alon na tumataas-baba)
        float strength = Mathf.Sin(time) * waveStrength;
        waterMaterial.SetFloat("_Strength", strength);

        // optional: animate texture movement
        waterMaterial.SetTextureOffset("_MainTex", new Vector2(time * 0.05f, time * 0.05f));

        // optional: scale wave tiling
        waterMaterial.SetFloat("_WaveScale", waveScale);
    }
}