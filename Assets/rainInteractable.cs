using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class rainInteractable : MonoBehaviour
{
    public GameObject rainEffectPrefab;  // Bu, sahnede disable edilmiş Rain objesi olmalı
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (rainEffectPrefab != null)
        {
            rainEffectPrefab.SetActive(true);
        }
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (rainEffectPrefab != null)
        {
            rainEffectPrefab.SetActive(false);
        }
    }
}