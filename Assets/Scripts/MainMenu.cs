using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject options;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public Slider musicVolumeSlider;
    public TMP_Text musicVolumeText;
    public Slider xSensSlider;
    public TMP_Text xSensText;
    public Slider ySensSlider;
    public TMP_Text ySensText;
    public Button playButton;
    public Button htpBackButton;
    public Button applyAllButton;

    public float timeBetween;
    public Image cutscene;
    public Sprite[] frames;

    public float interval;
    private float elapsedTime;
    public Image background;
    public Sprite[] bgFrames;
    private int frameNum;

    public void Awake()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        options.SetActive(false);
        cutscene.gameObject.SetActive(false);
        //AudioManager.Instance.Play("BackgroundMusic");
        playButton.Select();
        elapsedTime = 0;
        frameNum = 0;
    }

    public void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= interval)
        {
            if (frameNum >= (bgFrames.Length - 1))
            {
                frameNum = 0;
            }
            else
            {
                frameNum ++;
            }

            elapsedTime = 0;
            background.sprite = bgFrames[frameNum];
        }
    }

    public void PlayButton()
    {
        GameManager.Instance.win = false;
        //AudioManager.Instance.Stop("BackgroundMusic");
        
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        cutscene.gameObject.SetActive(true);
        for (int i = 0; i < frames.Length; i++)
        {
            cutscene.sprite = frames[i];

            yield return new WaitForSeconds(timeBetween);
        }

        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("Hub");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
        options.SetActive(false);
        htpBackButton.Select();
    }

    public void OptionsButton()
    {
        LoadValues();
        mainMenu.SetActive(false);
        howToPlay.SetActive(false);
        options.SetActive(true);
        applyAllButton.Select();
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        options.SetActive(false);
        playButton.Select();
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
