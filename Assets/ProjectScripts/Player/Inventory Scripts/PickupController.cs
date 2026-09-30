using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public Button pickupButton;

    [Header("Pickup Settings")]
    public float pickupDistance = 3f;

    private WoodPickup currentWood;
    private AxePickup currentAxe;
    private LighterPickup currentLighter;

    private void Start()
    {
        if (pickupButton != null)
        {
            pickupButton.gameObject.SetActive(false);
            pickupButton.onClick.AddListener(PickupCurrentItem);
        }
    }

    private void Update()
    {
        CheckForPickup();
    }

    private void CheckForPickup()
    {
        currentWood = null;
        currentAxe = null;
        currentLighter = null;

        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            pickupDistance,
            ~0,
            QueryTriggerInteraction.Ignore))
        {
            currentWood =
                hit.collider.GetComponentInParent<WoodPickup>();

            currentAxe =
                hit.collider.GetComponentInParent<AxePickup>();

            currentLighter =
                hit.collider.GetComponentInParent<LighterPickup>();
        }

        bool itemFound =
            currentWood != null ||
            currentAxe != null ||
            currentLighter != null;

        if (pickupButton != null)
        {
            pickupButton.gameObject.SetActive(itemFound);
        }
    }

    public void PickupCurrentItem()
    {
        // LIGHTER
        if (currentLighter != null)
        {
            currentLighter.PickUp();
            currentLighter = null;

            HidePickupButton();
            return;
        }

        // AXE
        if (currentAxe != null)
        {
            currentAxe.PickUp();
            currentAxe = null;

            HidePickupButton();
            return;
        }

        // WOOD
        if (currentWood != null)
        {
            currentWood.PickUp();
            currentWood = null;

            HidePickupButton();
            return;
        }
    }

    private void HidePickupButton()
    {
        if (pickupButton != null)
        {
            pickupButton.gameObject.SetActive(false);
        }
    }
}