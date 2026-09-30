using UnityEngine;

public class CampfireIgnition : MonoBehaviour
{
    [Header("Fire Objects")]
    public GameObject fireEffect;
    public GameObject fireLight;

    [Header("Requirements")]
    public int woodRequired = 3;

    private bool isLit = false;

    // Lets CampfireInteraction check if the fire is already lit
    public bool IsLit
    {
        get { return isLit; }
    }

    private void Start()
    {
        // Campfire starts unlit
        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
        }

        if (fireLight != null)
        {
            fireLight.SetActive(false);
        }
    }

    public void TryIgnite()
    {
        // Already lit
        if (isLit)
        {
            Debug.Log("Campfire is already lit!");
            return;
        }

        // Check EquipmentManager
        if (EquipmentManager.Instance == null)
        {
            Debug.LogWarning("EquipmentManager not found!");
            return;
        }

        // Player MUST have the lighter equipped
        if (!EquipmentManager.Instance.IsEquipped(
            ItemData.ItemType.Lighter))
        {
            Debug.Log("Equip the lighter first!");
            return;
        }

        // Check InventoryManager
        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("InventoryManager not found!");
            return;
        }

        // Check how much wood the player has
        int currentWood =
            InventoryManager.Instance.GetItemAmount(
                ItemData.ItemType.Wood
            );

        // Not enough wood
        if (currentWood < woodRequired)
        {
            Debug.Log(
                "Not enough wood! Need " +
                woodRequired +
                " wood. You have " +
                currentWood +
                "."
            );

            return;
        }

        // Remove the required wood
        bool removed =
            InventoryManager.Instance.RemoveItem(
                ItemData.ItemType.Wood,
                woodRequired
            );

        if (!removed)
        {
            Debug.LogWarning("Could not remove wood.");
            return;
        }

        // SUCCESS!
        isLit = true;

        if (fireEffect != null)
        {
            fireEffect.SetActive(true);
        }

        if (fireLight != null)
        {
            fireLight.SetActive(true);
        }

        Debug.Log("CAMPFIRE IGNITED!");
    }
}