using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void OpenSettings()
    {
        PlayerPrefs.SetString("SettingsReturnScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Settings");
    }

    public void OpenPreviousScene()
    {
        string scene = PlayerPrefs.GetString("SettingsReturnScene", "MainMenu");
        SceneManager.LoadScene(scene);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}