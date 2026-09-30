using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
        }
    }

    void ChangeMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();

        if (BgmMusicMainMenu.Instance != null)
        {
            BgmMusicMainMenu.Instance.SetVolume(volume);
        }
    }

    void ChangeSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
        PlayerPrefs.Save();
    }
}