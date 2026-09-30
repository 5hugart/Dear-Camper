using UnityEngine;

public class CampfireInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public FixedButton attackButton;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;

    private CampfireIgnition currentCampfire;
    private bool wasPressed = false;

    private void Start()
    {
        if (attackButton != null)
        {
            wasPressed = attackButton.Pressed;
        }
    }

    private void Update()
    {
        CheckForCampfire();
        CheckAttackButton();
    }

    private void CheckForCampfire()
    {
        currentCampfire = null;

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
            currentCampfire =
                hit.collider.GetComponentInParent<CampfireIgnition>();
        }
    }

    private void CheckAttackButton()
    {
        if (attackButton == null)
            return;

        bool pressed = attackButton.Pressed;

        if (pressed && !wasPressed)
        {
            TryIgnite();
        }

        wasPressed = pressed;
    }

    private void TryIgnite()
    {
        if (currentCampfire == null)
            return;

        if (currentCampfire.IsLit)
            return;

        if (EquipmentManager.Instance == null)
            return;

        if (!EquipmentManager.Instance.IsEquipped(
            ItemData.ItemType.Lighter))
        {
            return;
        }

        currentCampfire.TryIgnite();
    }
}