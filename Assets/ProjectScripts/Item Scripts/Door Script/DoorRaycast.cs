using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludelayerName = null;

    [SerializeField] private GameObject doorButton;

    private Door raycastedDoor;

    [SerializeField] private Image Crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "Door";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludelayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            // Door
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    raycastedDoor = hit.collider.gameObject.GetComponent<Door>();
                    CrosshairChange(true);
                }

                doorButton.SetActive(true);
                isCrosshairActive = true;
                doOnce = true;
            }
            else
            {
                doorButton.SetActive(false);
            }
        }

        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            Crosshair.color = Color.red;
        }
        else
        {
            Crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }

    public void Door()
    {
        raycastedDoor.PlayAnimationDoor();
    }
}