using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallBlocker : MonoBehaviour
{
    public Transform playerCamera;
    public float minDistance = 1.5f;
    public Color normalColor = Color.gray;
    public Color warningColor = Color.white;

    private MeshRenderer meshRenderer;
    private Material wallMaterial;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        wallMaterial = meshRenderer.material;
        wallMaterial.color = normalColor;
    }

    void Update()
    {
        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0;

        float currentDistance = direction.magnitude;

        if (currentDistance < minDistance)
        {
            Vector3 pushDir = direction.normalized;
            Vector3 newPos = transform.position + pushDir * minDistance;
            newPos.y = playerCamera.position.y;

            playerCamera.position = newPos;

            wallMaterial.color = warningColor;
        }
        else
        {
            wallMaterial.color = normalColor;
        }
    }
}

