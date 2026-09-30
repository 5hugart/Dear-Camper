using UnityEngine;

public class TreeInteractable : Interactable
{
    [Header("Tree Settings")]
    public int health = 5;

    [Header("Tree Parts")]
    public GameObject treeTop;
    public GameObject stump;

    [Header("Wood")]
    public GameObject woodPrefab;
    public int woodAmount = 3;

    private bool isCutDown = false;

    public void TakeDamage(int damage)
    {
        if (isCutDown)
            return;

        health -= damage;

        Debug.Log("Tree hit! HP: " + health);

        if (health <= 0)
        {
            CutDownTree();
        }
    }

    void CutDownTree()
    {
        if (isCutDown)
            return;

        isCutDown = true;

        Debug.Log("TREE CUT DOWN!");

        // Hide tree top
        if (treeTop != null)
        {
            treeTop.SetActive(false);
        }

        // Show stump
        if (stump != null)
        {
            stump.SetActive(true);
        }

        // Spawn wood
        if (woodPrefab != null)
        {
            for (int i = 0; i < woodAmount; i++)
            {
                Vector3 dropPosition =
                    transform.position +
                    new Vector3(
                        Random.Range(-1f, 1f),
                        0.5f,
                        Random.Range(-1f, 1f)
                    );

                Instantiate(
                    woodPrefab,
                    dropPosition,
                    Quaternion.identity
                );
            }
        }
    }

    public override void Interact()
    {
        TakeDamage(1);
    }
}