using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKeepDistance : MonoBehaviour
{
    public Transform playerCamera;
    public float minDistance = 0.03f; // 3 cm

    void Update()
    {
        // Y eksenini yok sayarak XZ düzleminde yön vektörü oluştur
        Vector3 direction = transform.position - playerCamera.position;
        direction.y = 0;  // Y eksenini sıfırla, sadece yatay mesafe hesaplanacak

        float currentDistance = direction.magnitude;

        if (currentDistance < minDistance)
        {
            // Yeni pozisyonu hesapla, Y ekseni değişmeden kalsın
            Vector3 newPos = playerCamera.position + direction.normalized * minDistance;
            newPos.y = transform.position.y; // Orijinal Y pozisyonunu koru

            transform.position = newPos;
        }
    }
}
