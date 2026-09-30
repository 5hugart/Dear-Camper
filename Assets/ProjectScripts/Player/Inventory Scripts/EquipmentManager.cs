using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    [Header("Axe Equipment")]
    public GameObject axeVisual;

    [Header("Lighter Equipment")]
    public GameObject lighterVisual;

    [Header("Mobile Controls")]
    public GameObject chopButton;

    private ItemData.ItemType? equippedItem = null;

    // Allows other scripts, such as the campfire,
    // to check which item is currently equipped.
    public ItemData.ItemType? EquippedItem
    {
        get { return equippedItem; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Unequip();
    }

    public void Equip(ItemData.ItemType itemType)
    {
        // Hide previous equipment first
        Unequip();

        equippedItem = itemType;

        switch (itemType)
        {
            case ItemData.ItemType.Axe:

                if (axeVisual != null)
                    axeVisual.SetActive(true);

                if (chopButton != null)
                    chopButton.SetActive(true);

                Debug.Log("AXE EQUIPPED!");
                break;


            case ItemData.ItemType.Lighter:

                if (lighterVisual != null)
                    lighterVisual.SetActive(true);

                if (chopButton != null)
                    chopButton.SetActive(true);

                Debug.Log("LIGHTER EQUIPPED!");
                break;
        }
    }

    public void Unequip()
    {
        equippedItem = null;

        if (axeVisual != null)
            axeVisual.SetActive(false);

        if (lighterVisual != null)
            lighterVisual.SetActive(false);

        if (chopButton != null)
            chopButton.SetActive(false);
    }

    public bool IsEquipped(ItemData.ItemType itemType)
    {
        return equippedItem.HasValue &&
               equippedItem.Value == itemType;
    }
}