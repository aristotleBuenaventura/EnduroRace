using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to an empty GameObject in your stormy map scenes (Intermediate, Pro).
/// Handles rain particles, fog, and ambient rain audio.
/// Does NOT need to be in Beginner scene at all.
/// </summary>
public class WeatherManager : MonoBehaviour
{
    [Header("Rain")]
    public ParticleSystem rainParticles;
    public float rainFadeInDuration = 3f;

    [Header("Fog")]
    public bool enableFog = true;
    public float fogDensity = 0.02f;
    public Color fogColor = new Color(0.6f, 0.6f, 0.65f);
    public float fogFadeInDuration = 5f;

    [Header("Ambient Audio")]
    public AudioSource rainAudioSource;
    public AudioClip rainLoopClip;
    [Range(0f, 1f)] public float rainAudioVolume = 0.4f;

    [Header("Lighting")]
    public Light sunLight;
    public float stormyLightIntensity = 0.3f;
    public Color stormyAmbientColor = new Color(0.3f, 0.3f, 0.35f);

    private float originalFogDensity;
    private Color originalFogColor;
    private bool originalFogEnabled;
    private float originalLightIntensity;
    private Color originalAmbientColor;

    private void Start()
    {
        // Save original settings so we can restore if needed
        originalFogEnabled = RenderSettings.fog;
        originalFogDensity = RenderSettings.fogDensity;
        originalFogColor = RenderSettings.fogColor;
        originalAmbientColor = RenderSettings.ambientLight;

        if (sunLight != null)
            originalLightIntensity = sunLight.intensity;

        // Start with rain off
        if (rainParticles != null)
        {
            var emission = rainParticles.emission;
            emission.rateOverTime = 0f;
            rainParticles.Stop();
        }

        if (rainAudioSource != null)
        {
            rainAudioSource.clip = rainLoopClip;
            rainAudioSource.loop = true;
            rainAudioSource.volume = 0f;
            rainAudioSource.playOnAwake = false;
        }

        // Start weather
        StartCoroutine(StartWeather());
    }

    private IEnumerator StartWeather()
    {
        // Small delay before weather kicks in
        yield return new WaitForSeconds(1f);

        // Enable fog
        if (enableFog)
        {
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogColor = fogColor;
        }

        // Start rain audio
        if (rainAudioSource != null && rainLoopClip != null)
            rainAudioSource.Play();

        // Start rain particles
        if (rainParticles != null)
            rainParticles.Play();

        // Fade everything in simultaneously
        float elapsed = 0f;
        float maxDuration = Mathf.Max(rainFadeInDuration, fogFadeInDuration);

        while (elapsed < maxDuration)
        {
            elapsed += Time.deltaTime;

            // Fade fog density
            if (enableFog)
            {
                float fogT = Mathf.Clamp01(elapsed / fogFadeInDuration);
                RenderSettings.fogDensity = Mathf.Lerp(0f, fogDensity, fogT);
            }

            // Fade rain audio
            if (rainAudioSource != null)
            {
                float audioT = Mathf.Clamp01(elapsed / rainFadeInDuration);
                rainAudioSource.volume = Mathf.Lerp(0f, rainAudioVolume, audioT);
            }

            // Fade rain particle emission rate
            if (rainParticles != null)
            {
                float rainT = Mathf.Clamp01(elapsed / rainFadeInDuration);
                var emission = rainParticles.emission;
                emission.rateOverTime = Mathf.Lerp(0f, 3000f, rainT);
            }

            // Dim lighting for stormy feel
            if (sunLight != null)
            {
                float lightT = Mathf.Clamp01(elapsed / fogFadeInDuration);
                sunLight.intensity = Mathf.Lerp(originalLightIntensity, stormyLightIntensity, lightT);
            }

            // Desaturate ambient light
            float ambientT = Mathf.Clamp01(elapsed / fogFadeInDuration);
            RenderSettings.ambientLight = Color.Lerp(originalAmbientColor, stormyAmbientColor, ambientT);

            yield return null;
        }

        // Snap to final values
        if (enableFog)
            RenderSettings.fogDensity = fogDensity;
        if (rainAudioSource != null)
            rainAudioSource.volume = rainAudioVolume;
        if (sunLight != null)
            sunLight.intensity = stormyLightIntensity;
        RenderSettings.ambientLight = stormyAmbientColor;

        ;
    }

    private void OnDestroy()
    {
        // Restore original settings when scene unloads
        RenderSettings.fog = originalFogEnabled;
        RenderSettings.fogDensity = originalFogDensity;
        RenderSettings.fogColor = originalFogColor;
        RenderSettings.ambientLight = originalAmbientColor;

        if (sunLight != null)
            sunLight.intensity = originalLightIntensity;
    }

    /// <summary>
    /// Call this to stop the weather (e.g. at race end)
    /// </summary>
    public void StopWeather()
    {
        StartCoroutine(StopWeatherRoutine());
    }

    private IEnumerator StopWeatherRoutine()
    {
        float elapsed = 0f;
        float fadeDuration = 2f;

        float startFogDensity = RenderSettings.fogDensity;
        float startAudioVolume = rainAudioSource != null ? rainAudioSource.volume : 0f;
        float startLightIntensity = sunLight != null ? sunLight.intensity : 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            if (enableFog)
                RenderSettings.fogDensity = Mathf.Lerp(startFogDensity, 0f, t);

            if (rainAudioSource != null)
                rainAudioSource.volume = Mathf.Lerp(startAudioVolume, 0f, t);

            if (sunLight != null)
                sunLight.intensity = Mathf.Lerp(startLightIntensity, originalLightIntensity, t);

            yield return null;
        }

        if (rainParticles != null)
            rainParticles.Stop();

        if (rainAudioSource != null)
            rainAudioSource.Stop();

        RenderSettings.fog = originalFogEnabled;
        RenderSettings.fogDensity = originalFogDensity;
    }
}