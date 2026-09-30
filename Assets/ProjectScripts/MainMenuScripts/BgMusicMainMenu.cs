using UnityEngine;

public class BgmMusicMainMenu : MonoBehaviour
{
    public static BgmMusicMainMenu Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSource.volume = volume;
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }
}