using UnityEngine;

public class AxePickup : MonoBehaviour
{
    private bool collected = false;

    public void PickUp()
    {
        if (collected)
            return;

        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning(
                "InventoryManager not found!"
            );

            return;
        }

        ItemData item =
            GetComponent<ItemData>();

        if (item == null)
        {
            Debug.LogWarning(
                "Axe has no ItemData component!"
            );

            return;
        }

        bool added =
            InventoryManager.Instance.AddItem(item);

        if (added)
        {
            collected = true;

            Debug.Log("AXE PICKED UP!");

            Destroy(gameObject);
        }
    }
}