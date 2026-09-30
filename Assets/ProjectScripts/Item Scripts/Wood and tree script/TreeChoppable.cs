using System.Collections;
using UnityEngine;

public class TreeChoppable : MonoBehaviour
{
    [Header("Tree Health")]
    public int maxHealth = 5;

    [Header("Tree Objects")]
    public GameObject fullTreeModel;
    public Transform fallPivot;
    public GameObject cutStump;

    [Header("Hit Reaction")]
    public float shakeAngle = 2f;
    public float shakeDuration = 0.12f;

    [Header("Falling")]
    public float fallAngle = 80f;
    public float fallDuration = 1.2f;
    public float disappearDelay = 0.7f;

    [Header("Wood Drops - Optional")]
    public GameObject woodDropPrefab;
    public int woodDropAmount = 3;
    public float dropRadius = 1f;

    private int currentHealth;

    private bool isFalling = false;
    private bool isShaking = false;

    private Quaternion originalPivotRotation;

    private void Start()
    {
        currentHealth = maxHealth;

        if (fallPivot != null)
        {
            originalPivotRotation = fallPivot.localRotation;
        }

        // Tree should be visible at the beginning.
        if (fullTreeModel != null)
        {
            fullTreeModel.SetActive(true);
        }

        // Cut stump should be hidden at the beginning.
        if (cutStump != null)
        {
            cutStump.SetActive(false);
        }
    }

    public void Chop(int damage)
    {
        if (isFalling)
            return;

        currentHealth -= damage;

        Debug.Log(
            "TREE HIT! Health: " +
            currentHealth +
            " / " +
            maxHealth
        );

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            StartCoroutine(FallTree());
        }
        else
        {
            if (!isShaking)
            {
                StartCoroutine(ShakeTree());
            }
        }
    }

    private IEnumerator ShakeTree()
    {
        if (fallPivot == null)
            yield break;

        isShaking = true;

        Quaternion startRotation = originalPivotRotation;

        Quaternion leftRotation =
            startRotation *
            Quaternion.Euler(0f, 0f, shakeAngle);

        Quaternion rightRotation =
            startRotation *
            Quaternion.Euler(0f, 0f, -shakeAngle);

        float halfDuration = shakeDuration / 2f;

        // Move one direction.
        float timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.Clamp01(timer / halfDuration);

            fallPivot.localRotation =
                Quaternion.Slerp(
                    startRotation,
                    leftRotation,
                    t
                );

            yield return null;
        }

        // Move back through the other direction.
        timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.Clamp01(timer / halfDuration);

            fallPivot.localRotation =
                Quaternion.Slerp(
                    rightRotation,
                    startRotation,
                    t
                );

            yield return null;
        }

        fallPivot.localRotation = startRotation;

        isShaking = false;
    }

    private IEnumerator FallTree()
    {
        if (fallPivot == null)
        {
            Debug.LogWarning("Tree has no FallPivot assigned!");
            yield break;
        }

        isFalling = true;

        // Wait for an existing shake to finish.
        while (isShaking)
        {
            yield return null;
        }

        Quaternion startRotation =
            fallPivot.localRotation;

        Quaternion endRotation =
            startRotation *
            Quaternion.Euler(
                fallAngle,
                0f,
                0f
            );

        float timer = 0f;

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;

            float t =
                Mathf.Clamp01(
                    timer / fallDuration
                );

            t = Mathf.SmoothStep(
                0f,
                1f,
                t
            );

            fallPivot.localRotation =
                Quaternion.Slerp(
                    startRotation,
                    endRotation,
                    t
                );

            yield return null;
        }

        fallPivot.localRotation =
            endRotation;

        // Let player see the fallen tree.
        yield return new WaitForSeconds(
            disappearDelay
        );

        // Hide original tree.
        if (fullTreeModel != null)
        {
            fullTreeModel.SetActive(false);
        }

        // Show leftover stump.
        if (cutStump != null)
        {
            cutStump.SetActive(true);
        }

        SpawnWood();

        Debug.Log("TREE CUT DOWN!");
    }

    private void SpawnWood()
    {
        if (woodDropPrefab == null)
            return;

        for (int i = 0; i < woodDropAmount; i++)
        {
            Vector2 randomPosition =
                Random.insideUnitCircle *
                dropRadius;

            Vector3 spawnPosition =
                transform.position +
                new Vector3(
                    randomPosition.x,
                    0.5f,
                    randomPosition.y
                );

            Instantiate(
                woodDropPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }
}