using System.Collections;
using UnityEngine;

public class TreeChopper : MonoBehaviour
{
    [Header("Player")]
    public Camera playerCamera;

    [Header("Mobile Button")]
    public FixedButton chopButton;

    [Header("Axe")]
    public Transform axe;
    public float axeSwingTime = 0.25f;

    [Header("Chopping")]
    public float chopDistance = 3f;
    public int chopDamage = 1;

    [Header("Layers")]
    public LayerMask treeLayer;

    private bool previousButtonState = false;
    private bool isChopping = false;

    private Quaternion axeStartRotation;

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (axe != null)
        {
            axeStartRotation = axe.localRotation;
        }
    }

    private void Update()
    {
        bool buttonPressed = false;

        if (chopButton != null)
        {
            buttonPressed = chopButton.Pressed;
        }

        // Detect the moment the button is pressed
        if (buttonPressed && !previousButtonState)
        {
            TryChop();
        }

        previousButtonState = buttonPressed;

        // PC testing
        if (Input.GetMouseButtonDown(0))
        {
            TryChop();
        }
    }

    private void TryChop()
    {
        if (isChopping)
            return;

        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            chopDistance,
            treeLayer
        ))
        {
            TreeChoppable tree =
                hit.collider.GetComponentInParent<TreeChoppable>();

            if (tree != null)
            {
                tree.Chop(chopDamage);

                StartCoroutine(SwingAxe());
            }
        }
    }

    private IEnumerator SwingAxe()
    {
        if (axe == null)
            yield break;

        isChopping = true;

        Quaternion startRotation = axe.localRotation;

        Quaternion swingRotation =
            startRotation * Quaternion.Euler(-70f, 0f, 0f);

        float timer = 0f;

        // Swing forward
        while (timer < axeSwingTime / 2f)
        {
            timer += Time.deltaTime;

            float t =
                timer / (axeSwingTime / 2f);

            axe.localRotation =
                Quaternion.Slerp(
                    startRotation,
                    swingRotation,
                    t
                );

            yield return null;
        }

        timer = 0f;

        // Return axe
        while (timer < axeSwingTime / 2f)
        {
            timer += Time.deltaTime;

            float t =
                timer / (axeSwingTime / 2f);

            axe.localRotation =
                Quaternion.Slerp(
                    swingRotation,
                    startRotation,
                    t
                );

            yield return null;
        }

        axe.localRotation = startRotation;

        isChopping = false;
    }
}