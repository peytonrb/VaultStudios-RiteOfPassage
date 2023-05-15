// handles pause menu functionality

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject howToPlay;
    public GameObject options;
    public GameObject background;
    public Button hptBackButton;
    public Button optionsButton;
    public Button optionsBackButton;
    public Button applyAllButton;
    public Button resumeButton;
    public TMP_Text mainHubText;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public Slider musicVolumeSlider;
    public TMP_Text musicVolumeText;
    public Slider xSensSlider;
    public TMP_Text xSensText;
    public Slider ySensSlider;
    public TMP_Text ySensText;
    public GameObject factUI;

    public void Awake()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
        background.SetActive(true);
        options.SetActive(false);
        factUI.SetActive(false);
    }

    public void OnEnable()
    {
        pauseMenu.SetActive(true);
        howToPlay.SetActive(false);
        background.SetActive(true);
        options.SetActive(false);
        factUI.SetActive(false);
        resumeButton.Select();
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.currentScene != "Hub")
            {
                mainHubText.text = "Hub";
            }
            else
            {
                mainHubText.text = "Main Menu";
            }
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance.currentScene != "Hub")
        {
            SceneManager.LoadScene("Hub");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        options.SetActive(false);
        howToPlay.SetActive(true);
        hptBackButton.Select();
    }

    public void OptionsButton()
    {
        pauseMenu.SetActive(false);
        options.SetActive(true);
        optionsBackButton.Select();
    }

    public void ResumeButton()
    {
        GameManager.Instance.resumeGame();
    }

    public void BackButton()
    {
        pauseMenu.SetActive(true);
        options.SetActive(false);
        resumeButton.Select();
    }

    public void HTPBackButton()
    {
        options.SetActive(true);
        howToPlay.SetActive(false);
        optionsBackButton.Select();
    }

    public void VolumeSlider(float volume)
    {
        volumeText.text = volume.ToString("0.0");
    }
    
    public void MusicVolumeSlider(float volume)
    {
        musicVolumeText.text = volume.ToString("0.0");
    }
    
    public void XSensSlider(float sensitivity)
    {
        xSensText.text = sensitivity.ToString("0.0");
    }
    
    public void YSensSlider(float sensitivity)
    {
        ySensText.text = sensitivity.ToString("0.0");
    }

    public void ApplyAllButton()
    {
        float volumeValue = volumeSlider.value;
        float musicVolumeValue = musicVolumeSlider.value;
        float xSensValue = xSensSlider.value;
        float ySensValue = ySensSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        AudioManager.Instance.SetVolume("CaveTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("CityTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("MainMenuTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("RedwoodTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("HubTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("WinTheme", musicVolumeValue);
        AudioManager.Instance.SetVolume("LoseTheme", musicVolumeValue);
        GameManager.Instance.xSens = xSensValue;
        GameManager.Instance.ySens = ySensValue;
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
        
        float musicVolumeValue = AudioManager.Instance.GetVolume("MainMenuTheme");
        musicVolumeSlider.value = musicVolumeValue;

        float xSensValue = GameManager.Instance.xSens;
        xSensSlider.value = xSensValue;

        float ySensValue = GameManager.Instance.ySens;
        ySensSlider.value = ySensValue;
    }
}
