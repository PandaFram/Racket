using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LowHealthEffects : MonoBehaviour
{
    [Header("Health Source")]
    public Slider healthSlider;


    [Header("Vignette Settings")]
    public Volume postProcessVolume;
    public float maxVignetteIntensity = 0.65f;

    [Header("Audio")]
    public AudioSource warningAudio;
    public float maxVolume = 1f; 
    public float minPitch = 0.9f;
    public float maxPitch = 1.3f;

    Vignette vignette;

    void Start()
    {
        postProcessVolume.profile.TryGet(out vignette);

        warningAudio.loop = true;
        warningAudio.volume = 0f;
        warningAudio.Play();
    }

    void Update()
    {
        if (healthSlider == null) return;
        float currentHealth = healthSlider.value;
        float maxHealth = healthSlider.maxValue;

        float healthPercent = currentHealth / maxHealth;
        float t = Mathf.InverseLerp(0.5f, 0f, healthPercent);
        //float healthPercent = currentHealth / maxHealth;

        //float t = Mathf.InverseLerp(0.5f, 0f, healthPercent);

        UpdateVignette(t);
        UpdateAudio(t);
    }

    void UpdateVignette(float t)
    {
        if (vignette == null) return;

        vignette.intensity.value = Mathf.Lerp(0f, maxVignetteIntensity, t);
    }

    void UpdateAudio(float t)
    {
        warningAudio.volume = Mathf.Lerp(0f, maxVolume, t);
        warningAudio.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
}
