using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup blackOverlay;
    public CanvasGroup title;

    [Header("Timing")]
    public float fadeInTime = 1.5f;
    public float titleFadeTime = 1.2f;
    public float holdTime = 1.5f;
    public float fadeOutTime = 1.5f;

    [Header("Scene")]
    public string nextScene = "MainMenu";

    void Start()
    {
        StartCoroutine(PlaySplash());
    }

    IEnumerator PlaySplash()
    {
        // Start completely black
        blackOverlay.alpha = 1f;
        title.alpha = 0f;

        // Fade in the campfire background
        yield return StartCoroutine(
            FadeCanvasGroup(blackOverlay, 1f, 0f, fadeInTime)
        );

        // Fade in title
        yield return StartCoroutine(
            FadeCanvasGroup(title, 0f, 1f, titleFadeTime)
        );

        // Hold
        yield return new WaitForSeconds(holdTime);

        // Fade everything out
        yield return StartCoroutine(
            FadeCanvasGroup(title, 1f, 0f, fadeOutTime)
        );

        yield return StartCoroutine(
            FadeCanvasGroup(blackOverlay, 0f, 1f, fadeOutTime)
        );

        // Load main menu
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator FadeCanvasGroup(
        CanvasGroup group,
        float start,
        float end,
        float duration
    )
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

            // Smooth easing
            t = t * t * (3f - 2f * t);

            group.alpha = Mathf.Lerp(start, end, t);

            yield return null;
        }

        group.alpha = end;
    }
}