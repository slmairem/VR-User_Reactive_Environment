using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShakeOnApproach : MonoBehaviour
{
    public Transform playerCamera; // XR Camera veya oyuncu kafası
    public float shakeDistance = 0.3f; // Yaklaşma mesafesi (örn: 30 cm)
    public float shakeAmount = 0.01f;  // Titreme miktarı
    public float shakeSpeed = 20f;     // Titreme hızı

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerCamera.position, transform.position);

        if (distance < shakeDistance)
        {
            // Titreme hareketi: pozisyonu periyodik olarak değiştir
            float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            float shakeY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;
            transform.localPosition = initialPosition + new Vector3(shakeX, shakeY, 0);
        }
        else
        {
            // Yakın değilse pozisyonu sıfırla
            transform.localPosition = initialPosition;
        }
    }
}

