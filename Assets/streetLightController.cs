using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetLightController : MonoBehaviour
{
    public Light sunLight; // Sahnedeki Directional Light (Güneş ışığı) referansı

    public List<GameObject> streetlightsRootObjects; // Sokak lambalarının ana GameObject'lerinin listesi

    [Range(0, 180)]
    public float sunsetAngleThreshold = 10f; // Güneşin batma açısı (örneğin 10 dereceye düşerse batmış sayılır)

    [Range(0, 180)]
    public float sunriseAngleThreshold = 170f; // Güneşin doğma açısı (örneğin 170 dereceye gelirse doğmuş sayılır)

    // Ortam Işığı Kontrolü İçin Yeni Değişkenler
    public Color dayAmbientColor = new Color(0.2f, 0.2f, 0.2f, 1f); // Gündüz ortam ışığı rengi
    public Color nightAmbientColor = new Color(0.05f, 0.05f, 0.1f, 1f); // Gece ortam ışığı rengi (koyu mavi tonu)
    public float ambientTransitionSpeed = 0.5f; // Ortam ışığı geçiş hızı

    private bool lightsOn = false;

    void Start()
    {
        // Başlangıçta lambaların ve ortam ışığının durumunu kontrol et
        UpdateEnvironment();
    }

    void Update()
    {
        UpdateEnvironment();
    }

    void UpdateEnvironment()
    {
        if (sunLight == null)
        {
            Debug.LogWarning("StreetlightController: Güneş ışığı (Directional Light) atanmamış! Lütfen atayın.");
            return;
        }

        float sunXRotation = sunLight.transform.eulerAngles.x;

        if (sunXRotation > 180f)
        {
            sunXRotation -= 360f;
        }

        bool shouldBeLightsOn = false;

        if (sunXRotation < sunsetAngleThreshold)
        {
            shouldBeLightsOn = true;
        }
        else if (sunXRotation > sunriseAngleThreshold)
        {
            shouldBeLightsOn = false;
        }

        // Lambaları güncelle
        if (shouldBeLightsOn && !lightsOn)
        {
            SetStreetlightsLightComponentsActive(true);
            lightsOn = true;
            Debug.Log("Güneş battı, sokak lambaları açıldı! (Açı: " + sunXRotation.ToString("F1") + ")");
        }
        else if (!shouldBeLightsOn && lightsOn)
        {
            SetStreetlightsLightComponentsActive(false);
            lightsOn = false;
            Debug.Log("Güneş doğdu, sokak lambaları kapandı! (Açı: " + sunXRotation.ToString("F1") + ")");
        }

        // Ortam Işığını Güncelle: RenderSettings.ambientLight'ı doğrudan kontrol et
        Color targetAmbientColor = shouldBeLightsOn ? nightAmbientColor : dayAmbientColor;
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetAmbientColor, Time.deltaTime * ambientTransitionSpeed);
    }

    void SetStreetlightsLightComponentsActive(bool active)
    {
        foreach (GameObject streetlightRoot in streetlightsRootObjects)
        {
            if (streetlightRoot != null)
            {
                Light[] lightComponents = streetlightRoot.GetComponentsInChildren<Light>(true);

                foreach (Light lightComp in lightComponents)
                {
                    if (lightComp.type == LightType.Point || lightComp.type == LightType.Spot)
                    {
                        lightComp.enabled = active;
                    }
                }
            }
        }
    }
}