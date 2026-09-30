using System.Collections;
using UnityEngine;

public class SleepFadeController : MonoBehaviour
{
    public static SleepFadeController Instance;

    [Header("Fade")]
    public CanvasGroup fadeCanvasGroup;

    [Header("Timing")]
    public float fadeDuration = 1f;
    public float blackScreenDuration = 1.5f;

    private bool isSleeping = false;

    private void Awake()
    {
        Instance = this;

        Debug.Log("SleepFadeController READY");
    }

    private void Start()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError(
                "FADE CANVAS GROUP IS NOT ASSIGNED!"
            );

            return;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;
    }

    public void StartSleep()
    {
        if (isSleeping)
            return;

        Debug.Log("SLEEP FADE STARTED");

        StartCoroutine(SleepSequence());
    }

    private IEnumerator SleepSequence()
    {
        isSleeping = true;

        fadeCanvasGroup.blocksRaycasts = true;

        // FADE TO BLACK
        Debug.Log("FADING TO BLACK");

        yield return StartCoroutine(
            FadeTo(1f)
        );

        Debug.Log("SCREEN IS BLACK");

        // Change the day/time while hidden
        if (DayNightCycleManager.Instance != null)
        {
            DayNightCycleManager.Instance.Sleep();
        }
        else
        {
            Debug.LogError(
                "DayNightCycleManager not found!"
            );
        }

        // Stay black briefly
        yield return new WaitForSeconds(
            blackScreenDuration
        );

        // FADE BACK
        Debug.Log("FADING BACK");

        yield return StartCoroutine(
            FadeTo(0f)
        );

        fadeCanvasGroup.blocksRaycasts = false;

        isSleeping = false;

        Debug.Log("SLEEP FADE FINISHED");
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha =
            fadeCanvasGroup.alpha;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress =
                Mathf.Clamp01(
                    elapsed / fadeDuration
                );

            fadeCanvasGroup.alpha =
                Mathf.Lerp(
                    startAlpha,
                    targetAlpha,
                    progress
                );

            yield return null;
        }

        fadeCanvasGroup.alpha =
            targetAlpha;
    }
}