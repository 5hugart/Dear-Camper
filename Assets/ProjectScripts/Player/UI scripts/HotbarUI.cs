using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    [System.Serializable]
    public class HotbarSlotUI
    {
        public Image itemIcon;
        public TMP_Text amountText;
    }

    [Header("Hotbar Slots")]
    public HotbarSlotUI[] slotUI = new HotbarSlotUI[4];

    private void Update()
    {
        RefreshHotbar();
    }

    // =====================================================
    // UPDATE HOTBAR ICONS AND AMOUNTS
    // =====================================================

    public void RefreshHotbar()
    {
        if (InventoryManager.Instance == null)
            return;

        for (int i = 0; i < slotUI.Length; i++)
        {
            InventoryManager.InventorySlot slot =
                InventoryManager.Instance.GetSlot(i);

            if (slot == null)
                continue;

            // -------------------------
            // EMPTY SLOT
            // -------------------------

            if (!slot.occupied)
            {
                if (slotUI[i].itemIcon != null)
                {
                    slotUI[i].itemIcon.gameObject.SetActive(false);
                }

                if (slotUI[i].amountText != null)
                {
                    slotUI[i].amountText.text = "";
                }

                continue;
            }

            // -------------------------
            // ITEM ICON
            // -------------------------

            if (slotUI[i].itemIcon != null)
            {
                slotUI[i].itemIcon.gameObject.SetActive(true);
                slotUI[i].itemIcon.sprite = slot.icon;
            }

            // -------------------------
            // ITEM AMOUNT
            // -------------------------

            if (slotUI[i].amountText != null)
            {
                if (slot.stackable && slot.amount > 1)
                {
                    slotUI[i].amountText.text =
                        "x" + slot.amount;
                }
                else
                {
                    slotUI[i].amountText.text = "";
                }
            }
        }
    }

    // =====================================================
    // SELECT / EQUIP HOTBAR SLOT
    // =====================================================

    public void SelectSlot(int index)
    {
        if (InventoryManager.Instance == null)
            return;

        InventoryManager.InventorySlot slot =
            InventoryManager.Instance.GetSlot(index);

        // -------------------------
        // EMPTY SLOT
        // -------------------------

        if (slot == null || !slot.occupied)
        {
            if (EquipmentManager.Instance != null)
            {
                EquipmentManager.Instance.Unequip();
            }

            Debug.Log(
                "Slot " +
                (index + 1) +
                " is empty."
            );

            return;
        }

        // -------------------------
        // EQUIP ITEM
        // -------------------------

        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.Equip(
                slot.itemType
            );
        }

        Debug.Log(
            "Selected Slot " +
            (index + 1) +
            ": " +
            slot.itemType
        );
    }
}