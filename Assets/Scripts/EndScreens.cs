using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreens : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;

    private void Update()
    {
        if(GameManager.Instance.win)
        {
            winText.SetActive(true);
            loseText.SetActive(false);
        }
        else
        {
            loseText.SetActive(true);
            winText.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
