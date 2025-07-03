using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightController : MonoBehaviour
{
    public Light sunLight; // Sahnedeki Directional Light (Güneş ışığı) referansı

    [Range(0, 180)]
    public float sunsetStartAngle = 20f; // Ortamın kararmaya başlayacağı güneş açısı (Örn: 20 dereceye düştüğünde)

    [Range(0, 180)]
    public float sunriseEndAngle = 160f; // Ortamın aydınlanmayı bitireceği güneş açısı (Örn: 160 dereceye çıktığında)

    // Gece ortamında olmasını istediğiniz minimum ortam ışığı rengi
    public Color nightAmbientColor = new Color(0.1f, 0.1f, 0.15f, 1f);

    public Color dayAmbientColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    public float ambientTransitionSpeed = 0.5f; 

    void Start()
    {
        // Başlangıçta ortam ışığının durumunu kontrol et
        UpdateAmbientLight();
    }

    void Update()
    {
        // Her karede ortam ışığının durumunu güncelle
        UpdateAmbientLight();
    }

    void UpdateAmbientLight()
    {
        if (sunLight == null)
        {
            Debug.LogWarning("AmbientLightController: Güneş ışığı (Directional Light) atanmamış! Lütfen atayın.");
            return;
        }

        float sunXRotation = sunLight.transform.eulerAngles.x;

        // Güneşin X rotasyonunu 0-360 yerine daha anlamlı bir aralığa çeviriyoruz (-180 ile 180).
        // Bu, sunset/sunrise eşiklerini ayarlamayı kolaylaştırır.
        if (sunXRotation > 180f)
        {
            sunXRotation -= 360f;
        }

        // Ortam ışığı için hedef rengi belirle
        Color targetAmbientColor;

        // Eğer güneş batış eşiğinin altındaysa (geceye yaklaşıyorsa) veya
        // güneş doğuş eşiğinin üstündeyse (gündüze yaklaşıyorsa)
        if (sunXRotation < sunsetStartAngle || sunXRotation > sunriseEndAngle)
        {
            targetAmbientColor = nightAmbientColor; // Hedef gece rengi
        }
        else
        {
            targetAmbientColor = dayAmbientColor; // Hedef gündüz rengi
        }

        // RenderSettings.ambientLight'ı (ortam aydınlatması) hedef renge doğru yumuşakça interpolate et
        // Bu, ani kararmayı veya aydınlanmayı engeller ve daha doğal bir geçiş sağlar.
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetAmbientColor, Time.deltaTime * ambientTransitionSpeed);
        
    }
}