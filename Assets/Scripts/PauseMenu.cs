using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject howToPlay;
    public GameObject background;

    public void Awake()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        pauseMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void ResumeButton()
    {
        GameManager.Instance.resumeGame();
    }

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
    }
}
