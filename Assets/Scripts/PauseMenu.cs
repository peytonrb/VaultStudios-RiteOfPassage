using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject howToPlay;
    public GameObject background;
    public Button hptBackButton;
    public Button resumeButton;

    public void Awake()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
        background.SetActive(true);
    }

    public void OnEnable()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
        background.SetActive(true);
        resumeButton.Select();
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
        hptBackButton.Select();
    }

    public void ResumeButton()
    {
        GameManager.Instance.resumeGame();
    }

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
        resumeButton.Select();
    }
}
