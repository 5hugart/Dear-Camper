using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadSelectMode()
    {
        SceneManager.LoadScene("SelectMode");
    }

    public void LoadGameMode()
    {
        SceneManager.LoadScene("GameMode");
    }

    public void LoadEasyScene()
    {
        SceneManager.LoadScene("EasyScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadIndex()
    {
        SceneManager.LoadScene("Index");
    }

    public void SelectEasy()
    {
        PlayerPrefs.SetString("Difficulty", "Easy");
        PlayerPrefs.Save();
        SceneManager.LoadScene("EasyMode");
    }

    public void SelectNormal()
    {
        PlayerPrefs.SetString("Difficulty", "Normal");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void SelectInsane()
    {
        PlayerPrefs.SetString("Difficulty", "Insane");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void SelectExtreme()
    {
        PlayerPrefs.SetString("Difficulty", "Extreme");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}