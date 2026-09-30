using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Hover Settings")]
    public float hoverScale = 1.08f;
    public float hoverMove = 8f;
    public float animationSpeed = 10f;

    [Header("Colors")]
    public Color normalColor = new Color(0.75f, 0.75f, 0.75f);
    public Color hoverColor = Color.white;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Vector3 targetScale;
    private Vector3 targetPosition;

    private TMP_Text text;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;

        targetScale = originalScale;
        targetPosition = originalPosition;

        text = GetComponentInChildren<TMP_Text>();

        if (text != null)
            text.color = normalColor;
    }

    void Update()
    {
        
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * animationSpeed
        );

        
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * animationSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        targetPosition = originalPosition +
                         new Vector3(hoverMove, 0f, 0f);

        if (text != null)
        {
            text.color = hoverColor;

            
            text.fontMaterial.EnableKeyword("GLOW_ON");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        targetPosition = originalPosition;

        if (text != null)
        {
            text.color = normalColor;
            text.fontMaterial.DisableKeyword("GLOW_ON");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
        transform.localScale = originalScale * 0.95f;
        targetScale = originalScale * hoverScale;
    }
}