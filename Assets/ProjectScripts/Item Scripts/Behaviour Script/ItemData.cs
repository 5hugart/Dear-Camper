using UnityEngine;

public class ItemData : MonoBehaviour
{ 
    public enum ItemType
    {
        Wood,
        Axe,
        Lighter,    
        Stone,
        Fish
    }

    [Header("Item Information")]
    public ItemType itemType;

    [Header("UI")]
    public Sprite itemIcon;

    [Header("Inventory")]
    public bool stackable = true;
    public int amount = 1;
}