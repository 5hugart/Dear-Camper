using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public FixedButton interactionButton;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;

    private BedInteractable currentBed;
    private bool wasPressed = false;

    private void Start()
    {
        if (interactionButton != null)
        {
            wasPressed = interactionButton.Pressed;
        }
    }

    private void Update()
    {
        CheckForBed();
        CheckInteractionButton();
    }

    // ==========================================
    // CHECK FOR BED
    // ==========================================

    private void CheckForBed()
    {
        currentBed = null;

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
            interactionDistance,
            ~0,
            QueryTriggerInteraction.Ignore))
        {
            currentBed =
                hit.collider.GetComponentInParent<BedInteractable>();
        }
    }

    // ==========================================
    // INTERACTION BUTTON
    // ==========================================

    private void CheckInteractionButton()
    {
        if (interactionButton == null)
            return;

        bool pressed = interactionButton.Pressed;

        // Only activate once per button press
        if (pressed && !wasPressed)
        {
            TryUseBed();
        }

        wasPressed = pressed;
    }

    // ==========================================
    // USE BED
    // ==========================================

    private void TryUseBed()
    {
        if (currentBed == null)
            return;

        currentBed.TrySleep();
    }

    // ==========================================
    // DEBUG
    // ==========================================

    private void OnDrawGizmosSelected()
    {
        if (playerCamera == null)
            return;

        Gizmos.DrawRay(
            playerCamera.transform.position,
            playerCamera.transform.forward *
            interactionDistance
        );
    }
}