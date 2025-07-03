using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyRotation : MonoBehaviour
{
    public float rotationSpeed = 6f; 

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}