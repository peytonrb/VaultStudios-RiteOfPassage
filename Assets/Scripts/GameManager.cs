using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    private GameObject PauseMenu;

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
    }

    private void Update()
    {
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
