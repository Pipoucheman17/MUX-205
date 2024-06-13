using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public void StartGame() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void CreditsButton() {
        SceneManager.LoadScene("Credits");
    }

    public void SettingsButton() {
        settingsWindow.SetActive(true);
    }

    public void closeSettingsWindow() {
        settingsWindow.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
