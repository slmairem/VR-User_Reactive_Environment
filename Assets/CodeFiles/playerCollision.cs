using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
{
    public Transform head;
    public Transform floorReference;

    CapsuleCollider myCollider;
    void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float height = head.position.y-floorReference.position.y;
        myCollider.height = height;
        transform.position = head.position-Vector3.up*height/2;
    }
}
