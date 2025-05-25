using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SkyRotation.cs
public class SkyRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
