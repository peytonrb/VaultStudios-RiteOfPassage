using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlay;

    public void Awake()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
    }

    public void PlayButton()
    {
        GameManager.Instance.win = false;
        SceneManager.LoadScene("Cave");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
    }
}
