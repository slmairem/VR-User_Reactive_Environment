using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonlightController : MonoBehaviour
{
    public Light sunLight;      // Güneş ışığı (Directional Light)
    public Light moonLight;     // Ay ışığı (Directional Light - sahneye manuel eklenmiş)

    [Range(0, 180)]
    public float sunsetStartAngle = 20f;

    [Range(0, 180)]
    public float sunriseEndAngle = 160f;

    public float transitionSpeed = 2f; // Geçiş yumuşaklığı

    void Start()
    {
        if (moonLight != null)
        {
            moonLight.enabled = false; // Başlangıçta kapalı olsun
        }
    }

    void Update()
    {
        if (sunLight == null || moonLight == null)
        {
            Debug.LogWarning("MoonlightController: Güneş ya da Ay ışığı eksik!");
            return;
        }

        float sunXRotation = sunLight.transform.eulerAngles.x;
        if (sunXRotation > 180f)
            sunXRotation -= 360f;

        // Gece mi?
        bool isNight = (sunXRotation < sunsetStartAngle || sunXRotation > sunriseEndAngle);

        // Işık geçişini yumuşat (fade in/out)
        float targetMoonIntensity = isNight ? 0.4f : 0f;
        moonLight.intensity = Mathf.Lerp(moonLight.intensity, targetMoonIntensity, Time.deltaTime * transitionSpeed);
        moonLight.enabled = moonLight.intensity > 0.01f; // Sıfıra çok yakınsa tamamen kapat
    }
}
