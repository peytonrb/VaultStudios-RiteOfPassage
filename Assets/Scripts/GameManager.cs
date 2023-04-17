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
    private string currentScene = "noScene";

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
            AudioManager.Instance.StopAll();
            currentScene = SceneManager.GetActiveScene().name;
            AudioManager.Instance.Play(currentScene + "Theme");
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

        if (photosCaptured >= 3 && Input.GetButtonDown("Interact") && !isDead)
        {
            win = true;
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene(2);
        }
        else if(isDead && SceneManager.GetActiveScene().buildIndex != 2)
        {
            win = false;
            isDead = false;
            AudioManager.Instance.StopAll();
            SceneManager.LoadScene(2);
        }

        if (SceneManager.GetActiveScene().name != "Cave")
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
}
