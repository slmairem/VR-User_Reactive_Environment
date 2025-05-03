using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class getMainLightDirection : MonoBehaviour
{
    [SerializeField] private Material skyboxMaterial;

    private void Update()
    {
        skyboxMaterial.SetVector("_mainLightDirection", transform.forward);
    }
}
