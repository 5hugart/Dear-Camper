using UnityEngine;

public class AxeController : MonoBehaviour
{
    [Header("Axe Settings")]
    public Animator animator;
    public FixedButton chopButton;

    [Header("Hit Settings")]
    public Camera playerCamera;
    public float hitDistance = 3f;
    public LayerMask treeLayer;

    [Header("Damage Settings")]
    public int chopDamage = 1;

    private bool wasPressed = false;
    private bool isSwinging = false;

    private void Start()
    {
        isSwinging = false;

        // Match the current button state so it doesn't count
        // as a new press when the scene starts.
        if (chopButton != null)
        {
            wasPressed = chopButton.Pressed;
        }

        if (animator != null)
        {
            // Clear any leftover trigger
            animator.ResetTrigger("Chop");

            // Force the axe to start in Idle
            animator.Play("Axe_Idle", 0, 0f);

            // Immediately update the Animator
            animator.Update(0f);
        }
    }

    private void Update()
    {
        if (chopButton == null)
            return;

        bool pressed = chopButton.Pressed;

        // Only swing once for each button press
        if (pressed && !wasPressed && !isSwinging)
        {
            StartChop();
        }

        wasPressed = pressed;
    }

    public void StartChop()
    {
        if (isSwinging)
            return;

        if (animator == null)
        {
            Debug.LogWarning("Axe Animator is not assigned!");
            return;
        }

        isSwinging = true;

        animator.ResetTrigger("Chop");
        animator.SetTrigger("Chop");

        Debug.Log("AXE SWING STARTED");
    }

    // Called by Animation Event at the impact frame
    public void HitTree()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning(
                "Player Camera is not assigned!"
            );

            return;
        }

        Debug.Log("AXE IMPACT!");

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            hitDistance,
            treeLayer,
            QueryTriggerInteraction.Ignore))
        {
            Debug.Log(
                "Ray hit: " +
                hit.collider.name
            );

            TreeChoppable tree =
                hit.collider
                .GetComponentInParent<TreeChoppable>();

            if (tree != null)
            {
                tree.Chop(chopDamage);

                Debug.Log("TREE CHOPPED!");
            }
        }
    }

    // Called by Animation Event near the end of Axe_Chop
    public void FinishChop()
    {
        isSwinging = false;

        Debug.Log("AXE SWING FINISHED");
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCamera == null)
            return;

        Gizmos.DrawRay(
            playerCamera.transform.position,
            playerCamera.transform.forward * hitDistance
        );
    }
}