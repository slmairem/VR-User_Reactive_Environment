using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedLightController : MonoBehaviour
{
    [Header("Lights")]
    public Light sunLight;
    public Light moonLight;

    [Header("Ambient Light Settings")]
    [Range(0, 180)]
    public float sunsetStartAngle = 20f;
    [Range(0, 180)]
    public float sunriseEndAngle = 160f;

    public Color nightAmbientColor = new Color(0.1f, 0.1f, 0.15f, 1f);
    public Color dayAmbientColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public float ambientTransitionSpeed = 0.5f;

    [Header("Moon Light Settings")]
    public float moonTransitionSpeed = 2f;
    public float moonMaxIntensity = 0.4f;

    [Header("Skyboxes")]
    public Material proceduralSkybox;  // Gündüz Skybox
    public Material nightSkybox;       // Gece Skybox

    private List<Light> streetLights = new List<Light>();
    private bool isCurrentlyNight = false;

    void Start()
    {
        if (moonLight != null)
        {
            moonLight.enabled = false;
            moonLight.intensity = 0f;
        }

        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");
        foreach (GameObject lightObj in lightObjects)
        {
            Light light = lightObj.GetComponent<Light>();
            if (light != null && light != sunLight && light != moonLight)
            {
                streetLights.Add(light);
                light.enabled = false;
            }
        }

        // Gündüz skybox varsayılan olarak atanmalı
        RenderSettings.skybox = proceduralSkybox;
    }

    void Update()
    {
        if (sunLight == null || moonLight == null)
        {
            Debug.LogWarning("Güneş veya Ay ışığı atanmadı.");
            return;
        }

        float sunXRotation = sunLight.transform.eulerAngles.x;
        if (sunXRotation > 180f) sunXRotation -= 360f;
        bool isNight = (sunXRotation < sunsetStartAngle || sunXRotation > sunriseEndAngle);

        // Skybox geçişi
        if (isNight && !isCurrentlyNight)
        {
            RenderSettings.skybox = nightSkybox;
            isCurrentlyNight = true;
        }
        else if (!isNight && isCurrentlyNight)
        {
            RenderSettings.skybox = proceduralSkybox;
            isCurrentlyNight = false;
        }

        // Ambient ışık geçişi
        Color targetAmbient = isNight ? nightAmbientColor : dayAmbientColor;
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetAmbient, Time.deltaTime * ambientTransitionSpeed);

        // Moonlight geçişi (ışığı aç/kapat + intensity)
        float targetMoonIntensity = isNight ? moonMaxIntensity : 0f;
        moonLight.intensity = Mathf.Lerp(moonLight.intensity, targetMoonIntensity, Time.deltaTime * moonTransitionSpeed);

        // Ay ışığı sadece gece açık
        moonLight.enabled = isNight;

        // Sokak lambaları (moonLight açıkken onlar da açık)
        foreach (var light in streetLights)
        {
            if (light != null)
                light.enabled = isNight;
        }
    }
}
