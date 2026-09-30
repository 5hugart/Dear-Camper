using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string itemName = "Object";

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + itemName);
    }
}