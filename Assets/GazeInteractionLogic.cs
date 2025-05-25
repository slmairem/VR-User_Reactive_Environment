using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteractionLogic : MonoBehaviour
{
    public float gazeTimeThreshold = 2f;  // Bakma süresi (tetikleme)
    public float moveDuration = 3f;       // Hareket süresi
    public float moveSpeed = 1f;          // Hareket hızı
    public Material hoverMat;
    public Material selectMat;

    private Material originalMat;
    private Renderer rend;

    private bool isGazedAt = false;
    private float gazeTimer = 0f;
    private bool isMoving = false;
    private float moveTimer = 0f;

    private Vector3 targetPosition;
    private bool hoverEffectActive = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isGazedAt && !isMoving)
        {
            gazeTimer += Time.deltaTime;

            // Hover efekti yalnızca bakmaya başlayınca aktifleşsin (örnek: 0.1 saniye sonra)
            if (!hoverEffectActive && gazeTimer > 0.1f)
            {
                ActivateHoverEffect();
                hoverEffectActive = true;
            }

            if (gazeTimer >= gazeTimeThreshold)
            {
                Ray gazeRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if (Physics.Raycast(gazeRay, out RaycastHit hit))
                {
                    targetPosition = hit.point;
                    isMoving = true;
                    moveTimer = 0f;

                    if (selectMat != null)
                        rend.material = selectMat;
                }
            }
        }

        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer < moveDuration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public void OnHoverEnter()
    {
        isGazedAt = true;
        gazeTimer = 0f;
        hoverEffectActive = false;
    }

    public void OnHoverExit()
    {
        isGazedAt = false;
        gazeTimer = 0f;
        hoverEffectActive = false;
        rend.material = originalMat;
    }

    private void ActivateHoverEffect()
    {
        if (hoverMat != null)
            rend.material = hoverMat;
        else
            rend.material.color = originalMat.color * 1.2f;
    }

    public void OnSelect() { } // Gerekirse kullan
}