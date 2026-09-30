using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmbienceZone : MonoBehaviour
{
    [SerializeField] private AudioClip zoneAmbience;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AmbienceManager.Instance.PlayAmbience(zoneAmbience);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            AmbienceManager.Instance.PlayDefault();
    }
}