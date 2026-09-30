using System.Collections;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager Instance;

    [SerializeField] private AudioClip defaultAmbience;
    [SerializeField] private float fadeTime = 2f;

    private AudioSource sourceA;
    private AudioSource sourceB;
    private AudioSource active;
    private AudioClip currentClip;
    private Coroutine fadeRoutine;
    private float volume;

    void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat("SfxVolume", 1f);

        sourceA = gameObject.AddComponent<AudioSource>();
        sourceB = gameObject.AddComponent<AudioSource>();
        foreach (var s in new[] { sourceA, sourceB })
        {
            s.loop = true;
            s.playOnAwake = false;
            s.spatialBlend = 0f;
            s.volume = 0f;
        }
        active = sourceA;
    }

    void Start()
    {
        PlayAmbience(defaultAmbience);
    }

    // Called by the sound effects slider
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        if (fadeRoutine == null && active != null)
            active.volume = volume;
    }

    public void PlayAmbience(AudioClip clip)
    {
        if (clip == null || clip == currentClip) return;
        currentClip = clip;

        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Crossfade(clip));
    }

    public void PlayDefault()
    {
        PlayAmbience(defaultAmbience);
    }

    IEnumerator Crossfade(AudioClip clip)
    {
        AudioSource from = active;
        AudioSource to = (active == sourceA) ? sourceB : sourceA;
        active = to;

        to.clip = clip;
        to.volume = 0f;
        to.Play();

        float startFrom = from.volume;
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            float k = t / fadeTime;
            from.volume = Mathf.Lerp(startFrom, 0f, k);
            to.volume = Mathf.Lerp(0f, volume, k);
            yield return null;
        }

        from.Stop();
        from.volume = 0f;
        to.volume = volume;
        fadeRoutine = null;
    }
}