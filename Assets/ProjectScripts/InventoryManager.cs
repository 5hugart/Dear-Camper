using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class InventorySlot
    {
        public ItemData.ItemType itemType;
        public Sprite icon;
        public int amount;
        public bool occupied;
        public bool stackable;
    }

    [Header("Hotbar")]
    public InventorySlot[] slots = new InventorySlot[4];

    private void Awake()
    {
        Debug.Log(
            "InventoryManager Awake on: " +
            gameObject.name
        );

        if (Instance == null)
        {
            Instance = this;

            Debug.Log(
                "InventoryManager Instance successfully created!"
            );
        }
        else if (Instance != this)
        {
            Debug.LogWarning(
                "DUPLICATE InventoryManager found on: " +
                gameObject.name
            );

            Destroy(gameObject);
        }
    }

    // =====================================================
    // ADD ITEM
    // =====================================================

    public bool AddItem(ItemData item)
    {
        if (item == null)
            return false;

        // Stack existing item
        if (item.stackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].occupied &&
                    slots[i].itemType == item.itemType)
                {
                    slots[i].amount += item.amount;

                    Debug.Log(
                        item.itemType +
                        " increased to x" +
                        slots[i].amount
                    );

                    return true;
                }
            }
        }

        // Find empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].occupied)
            {
                slots[i].occupied = true;
                slots[i].itemType = item.itemType;
                slots[i].icon = item.itemIcon;
                slots[i].amount = item.amount;
                slots[i].stackable = item.stackable;

                Debug.Log(
                    item.itemType +
                    " added to Slot " +
                    (i + 1)
                );

                return true;
            }
        }

        Debug.Log("HOTBAR FULL!");
        return false;
    }

    // =====================================================
    // GET SLOT
    // =====================================================

    public InventorySlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Length)
        {
            return null;
        }

        return slots[index];
    }

    // =====================================================
    // GET ITEM AMOUNT
    // =====================================================

    public int GetItemAmount(ItemData.ItemType itemType)
    {
        int totalAmount = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].occupied &&
                slots[i].itemType == itemType)
            {
                totalAmount += slots[i].amount;
            }
        }

        return totalAmount;
    }

    // =====================================================
    // REMOVE ITEM
    // =====================================================

    public bool RemoveItem(
        ItemData.ItemType itemType,
        int amount)
    {
        int currentAmount = GetItemAmount(itemType);

        if (currentAmount < amount)
        {
            Debug.Log(
                "Not enough " +
                itemType +
                " to remove!"
            );

            return false;
        }

        int amountLeftToRemove = amount;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].occupied)
                continue;

            if (slots[i].itemType != itemType)
                continue;

            int removeAmount =
                Mathf.Min(
                    slots[i].amount,
                    amountLeftToRemove
                );

            slots[i].amount -= removeAmount;
            amountLeftToRemove -= removeAmount;

            // Clear slot if empty
            if (slots[i].amount <= 0)
            {
                slots[i].amount = 0;
                slots[i].occupied = false;
                slots[i].icon = null;
                slots[i].stackable = false;
            }

            if (amountLeftToRemove <= 0)
            {
                break;
            }
        }

        Debug.Log(
            "Removed " +
            amount +
            " " +
            itemType
        );

        return true;
    }
}