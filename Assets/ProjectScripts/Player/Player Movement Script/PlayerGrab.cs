using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Camera playerCamera;
    public FixedButton grabButton;

    public float interactDistance = 3f;
    public LayerMask interactableLayer;

    private bool wasPressed = false;

    void Update()
    {
        bool pressed = grabButton != null && grabButton.Pressed;

        // Only interact once per button press
        if (pressed && !wasPressed)
        {
            TryInteract();
        }

        wasPressed = pressed;
    }

    void TryInteract()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            interactDistance,
            interactableLayer))
        {
            Interactable interactable =
                hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}