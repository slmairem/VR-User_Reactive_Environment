using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeFollowMovement : MonoBehaviour
{
    public float gazeThreshold = 1f;         // Bakış süresi eşiği
    public float moveDuration = 2f;          // Hareket süresi
    public float moveSpeed = 1f;             // Hareket hızı
    public Material hoverMat;

    private Material originalMat;
    private Renderer rend;

    private bool isGazedAt = false;
    private float gazeTimer = 0f;
    private bool isMoving = false;
    private float moveTimer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;
    }

    void Update()
    {
        if (isGazedAt && !isMoving)
        {
            gazeTimer += Time.deltaTime;

            if (gazeTimer >= gazeThreshold)
            {
                if (hoverMat != null)
                    rend.material = hoverMat;

                isMoving = true;
                moveTimer = 0f;
            }
        }

        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            // Kameranın güncel ileri yönü (sadece XZ düzlemi)
            Vector3 gazeDir = Camera.main.transform.forward;
            Vector3 flatDir = new Vector3(gazeDir.x, 0f, gazeDir.z).normalized;

            // Hareket uygula (Y ekseni sabit)
            Vector3 newPosition = transform.position + flatDir * moveSpeed * Time.deltaTime;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            if (moveTimer >= moveDuration)
            {
                isMoving = false;
            }
        }
    }

    public void OnHoverEnter()
    {
        isGazedAt = true;
        gazeTimer = 0f;
        isMoving = false;
        moveTimer = 0f;
    }

    public void OnHoverExit()
    {
        isGazedAt = false;
        gazeTimer = 0f;
        isMoving = false;
        moveTimer = 0f;

        if (originalMat != null)
            rend.material = originalMat;
    }
}
