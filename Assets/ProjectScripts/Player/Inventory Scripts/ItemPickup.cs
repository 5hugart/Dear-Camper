using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public int hitsRequired = 5;
    private int currentHits = 0;

    public GameObject woodPrefab;
    public int woodAmount = 3;

    public void Chop()
    {
        currentHits++;

        Debug.Log("Tree chopped: " + currentHits + "/" + hitsRequired);

        if (currentHits >= hitsRequired)
        {
            CutDownTree();
        }
    }

    private void CutDownTree()
    {
        // Spawn wood
        if (woodPrefab != null)
        {
            for (int i = 0; i < woodAmount; i++)
            {
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 1f;
                spawnPosition.y = transform.position.y + 0.5f;

                Instantiate(woodPrefab, spawnPosition, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}