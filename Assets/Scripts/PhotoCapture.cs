using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private Image photoDisplayUI1;
    [SerializeField] private Image photoDisplayUI2;
    [SerializeField] private Image photoDisplayUI3;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject photoCam;
    [SerializeField] private GameObject flash;
    [SerializeField] private GameObject[] photoOrbs;
    private PhotoCameraController photoCamController;
    public GameObject photoHolder;
    private Renderer rend;
    private Texture2D screenCapture1;
    private Texture2D screenCapture2;
    private Texture2D screenCapture3;
    public bool viewingPhoto;
    private bool rtPressed;
    public bool newPhoto;

    private void Start()
    {
        newPhoto = false;
        screenCapture1 = new Texture2D(720, 720, TextureFormat.RGB24, false);
        screenCapture2 = new Texture2D(720, 720, TextureFormat.RGB24, false);
        screenCapture3 = new Texture2D(720, 720, TextureFormat.RGB24, false);
        if(photoHolder != null)
        {
            rend = photoHolder.GetComponent<Renderer>();
        }
        
        mainUI.SetActive(true);
        photoCamController = photoCam.GetComponent<PhotoCameraController>();
    }

    private void OnEnable()
    {
        cameraUI.SetActive(true);
        mainUI.SetActive(false);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Stop("FootStepSound");
        }

        foreach (GameObject orb in photoOrbs)
        {
            orb.SetActive(true);
        }
    }

    private void OnDisable()
    {
        cameraUI.SetActive(false);
        photoFrame.SetActive(false);
        viewingPhoto = false;

        foreach (GameObject orb in photoOrbs)
        {
            if (orb != null)
            {
                orb.SetActive(false);
            }
        }
    }

    private void Update()
    {
        float rightTriggerValue = Input.GetAxis("TakePicture");

        if (Input.GetButtonDown("TakePicture"))
        {      
            if(!viewingPhoto)
            {
                if (GameManager.Instance.savePhoto)
                {
                    StartCoroutine(CapturePhoto());
                }
            }
            else
            {
                RemovePhoto();
            }
        }
        else if (rightTriggerValue == 1 && !rtPressed)
        {
            rtPressed = true;
            if(!viewingPhoto)
            {
                if (GameManager.Instance.savePhoto)
                {
                    StartCoroutine(CapturePhoto());
                }
            }
            else
            {
                RemovePhoto();
            }
        }
        else if (rightTriggerValue < 1 && rtPressed)
        {
            rtPressed = false;
        }

        if (GameManager.Instance.resetUIPics)
        {
            ResetUI();
        }
    }

    IEnumerator CapturePhoto()
    {
        GameManager.Instance.savePhoto = false;
        viewingPhoto = true;
        cameraUI.SetActive(false);
        mainUI.SetActive(false);
        photoCamController.enabled = false;
        flash.SetActive(true);

        foreach (GameObject orb in photoOrbs)
        {
            orb.SetActive(false);
        }

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect ((Screen.width/2) - 360, (Screen.height/2) - 360, 720, 720);

        if (GameManager.Instance.activeSpot == 1)
        {
            screenCapture1.ReadPixels(regionToRead, 0, 0, false);
            screenCapture1.Apply();
            byte[] bytes1 = screenCapture1.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameManager.Instance.activeSpot + ".png", bytes1);
        }
        else if (GameManager.Instance.activeSpot == 2)
        {
            screenCapture2.ReadPixels(regionToRead, 0, 0, false);
            screenCapture2.Apply();
            byte[] bytes2 = screenCapture2.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameManager.Instance.activeSpot + ".png", bytes2);
        }
        else if (GameManager.Instance.activeSpot == 3)
        {
            screenCapture3.ReadPixels(regionToRead, 0, 0, false);
            screenCapture3.Apply();
            byte[] bytes3 = screenCapture3.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameManager.Instance.activeSpot + ".png", bytes3);
        }


        if (photoHolder != null)
        {
            rend.material = new Material(Shader.Find("Standard"));
            if (GameManager.Instance.activeSpot == 1)
            {
                rend.material.mainTexture = screenCapture1;
            }
            else if (GameManager.Instance.activeSpot == 2)
            {
                rend.material.mainTexture = screenCapture2;
            }
            else if (GameManager.Instance.activeSpot == 3)
            {
                rend.material.mainTexture = screenCapture3;
            }
        }

        
        foreach (GameObject orb in photoOrbs)
        {
            orb.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
        newPhoto = true;

        ShowPhoto();
        photoCamController.enabled = true;
    }

    void ShowPhoto()
    {
        Color white = new Color(1f, 1f, 1f, 1f);

        if (GameManager.Instance.activeSpot == 1)
        {
            Sprite photoSprite1 = Sprite.Create(screenCapture1, new Rect(0.0f, 0.0f, screenCapture1.width, screenCapture1.height), new Vector2(0.5f, 0.5f), 100.0f);
            photoDisplayArea.sprite = photoSprite1;
            photoDisplayUI1.sprite = photoSprite1;
            photoDisplayUI1.color = white;
        }
        else if (GameManager.Instance.activeSpot == 2)
        {
            Sprite photoSprite2 = Sprite.Create(screenCapture2, new Rect(0.0f, 0.0f, screenCapture2.width, screenCapture2.height), new Vector2(0.5f, 0.5f), 100.0f);
            photoDisplayArea.sprite = photoSprite2;
            photoDisplayUI2.sprite = photoSprite2;
            photoDisplayUI2.color = white;
        }
        else if (GameManager.Instance.activeSpot == 3)
        {
            Sprite photoSprite3 = Sprite.Create(screenCapture3, new Rect(0.0f, 0.0f, screenCapture3.width, screenCapture3.height), new Vector2(0.5f, 0.5f), 100.0f);
            photoDisplayArea.sprite = photoSprite3;
            photoDisplayUI3.sprite = photoSprite3;
            photoDisplayUI3.color = white;
        }

        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
    } 

    void ResetUI()
    {
        GameManager.Instance.resetUIPics = false;
        photoDisplayUI1.sprite = null;
        photoDisplayUI2.sprite = null;
        photoDisplayUI3.sprite = null;
    }
}
