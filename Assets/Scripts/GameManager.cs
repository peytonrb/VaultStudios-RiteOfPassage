using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    public GameObject PauseMenu;
    public int photosCaptured;
    public bool win;
    public bool isDead;
    public int activeSpot;
    public bool savePhoto;
    public float xSens;
    public float ySens;
    public string currentScene = "noScene";
    public bool caveW;
    public bool cityW;
    public bool redwoodW;
    public bool resetUIPics;
    public bool spot1;
    public bool spot2;
    public bool spot3;
    public float stam;
    public float maxStam;
    public bool gliding;
    public bool grounded;

    private void Awake()
    {
        // This moves the current GameManager to DontDestroyOnLoad and deletes any other GameManagers
        if (Instance != null) 
        {
            DestroyImmediate(gameObject);
        } 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
            savePhoto = false;
            win = false;
    }

    private void OnDisable()
    {
        this.enabled = true;
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name != currentScene)
        {
            resetUIPics = true;
            AudioManager.Instance.StopAll();
            currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "EndScreen")
            {
                if (win)
                {
                    AudioManager.Instance.Play("FinalCutsceneTheme");
                }
                else
                {
                    AudioManager.Instance.Play("LoseTheme");
                    //AudioManager.Instance.Play("FinalCutsceneTheme");
                }
            }
            else
            {
                AudioManager.Instance.Play(currentScene + "Theme");
            }
            
        }

        if(PauseMenu == null)
        {
            PauseMenu = GameObject.Find("PauseMenu");
            if(PauseMenu != null)
            {
                PauseMenu.SetActive(false);
            }
        }

        if(Input.GetButtonDown("Pause"))
        {
            if(!PauseMenu.activeSelf)
            {
                pauseGame();
            }
            else
            {
                resumeGame();
            }
        }

        if (photosCaptured >= 3 && !isDead)
        {
            //AudioManager.Instance.StopAll();

            if (currentScene == "Cave")
            {
                caveW = true;
            }
            else if (currentScene == "redwood")
            {
                redwoodW = true;
            }
            else if (currentScene == "City")
            {
                cityW = true;
            }

            if (caveW && redwoodW && cityW)
            {
                win = true;
                //SceneManager.LoadScene("EndScreen");
            }
            else
            {
                //SceneManager.LoadScene("Hub");
            }
        }
        else if(isDead)
        {
            win = false;
            isDead = false;
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene("EndScreen");
        }

        if (SceneManager.GetActiveScene().name == "Hub")
        {
            photosCaptured = 0;
        }
    }
    
    void Start()
    {
        LockCursor();
    }

    public void pauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);

        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Portal(string level)
    {
        SceneManager.LoadScene(level);
    }
}
