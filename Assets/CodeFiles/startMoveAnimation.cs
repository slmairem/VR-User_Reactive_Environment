using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMoveAnimation : MonoBehaviour
{
    public Transform playerCamera;
    public float minDistance = 0.03f;
    public Animator animator;
    public string moveTriggerName = "startMoveTrigger";
    public string runBoolName = "isRunning";
    public string animIndexName = "AnimIndex"; // AnimIndex parametresi için string değişken

    // Karakterin ne kadar hızlı döneceğini kontrol eder
    public float rotationSpeed = 10f;

    private float previousDistance;

    void Start()
    {
        previousDistance = Vector3.Distance(GetFlatPosition(transform.position), GetFlatPosition(playerCamera.position));
    }

    void Update()
    {
        Vector3 flatObjectPos = GetFlatPosition(transform.position);
        Vector3 flatCameraPos = GetFlatPosition(playerCamera.position);

        Vector3 direction = flatObjectPos - flatCameraPos; // Kameradan karaktere doğru yön
        float currentDistance = direction.magnitude;

        if (animator != null)
        {
            if (currentDistance < minDistance) // Karakter kameraya çok yaklaştıysa ve itilmesi gerekiyorsa
            {
                // Animasyonları ayarla
                animator.SetTrigger(moveTriggerName);
                animator.SetBool(runBoolName, true);
                animator.SetInteger(animIndexName, 1);

                // Kamera ile nesne arasındaki minimum mesafeyi koruma
                Vector3 newPos = flatCameraPos + direction.normalized * minDistance;
                newPos.y = transform.position.y;
                transform.position = newPos;

                // Karakteri hareket ettiği yöne doğru döndür
                // Karakterin itildiği yön 'direction.normalized' yönüdür.
                // Yere paralel bir rotasyon için Y ekseni etrafında döneceğiz.
                if (direction.sqrMagnitude > 0.001f) // Yeterince büyük bir yön vektörü varsa dön
                {
                    // Sadece yatay düzlemdeki yönü al (y eksenini yok say)
                    Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z).normalized;

                    // Hedef rotasyonu hesapla
                    Quaternion targetRotation = Quaternion.LookRotation(flatDirection);

                    // Karakteri hedefe doğru yumuşakça döndür
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else // Karakter yeterince uzakta ve itilmiyor
            {
                if (animator.GetBool(runBoolName) == true)
                {
                    animator.SetBool(runBoolName, false);
                    animator.SetInteger(animIndexName, 0);
                }
            }
        }

        previousDistance = currentDistance;
    }

    Vector3 GetFlatPosition(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }
}