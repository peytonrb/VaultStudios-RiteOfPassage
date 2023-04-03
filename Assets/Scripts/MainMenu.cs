using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlay;

    public void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Awake()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        AudioManager.Instance.Play("BackgroundMusic");
    }

    public void PlayButton()
    {
        GameManager.Instance.win = false;
        AudioManager.Instance.Stop("BackgroundMusic");
        SceneManager.LoadScene(1);
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
