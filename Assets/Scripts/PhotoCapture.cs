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
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject mainUI;
    //public Shader shader;
    public GameObject photoHolder;
    private Renderer rend;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private bool used = false;
    public float axis;

    private void Start()
    {
        screenCapture = new Texture2D(720, 720, TextureFormat.RGB24, false);
        if(photoHolder != null)
        {
            rend = photoHolder.GetComponent<Renderer>();
        }
        
        Debug.Log(Application.persistentDataPath);
    }

    private void OnEnable()
    {
        cameraUI.SetActive(true);
        mainUI.SetActive(false);
    }

    private void OnDisable()
    {
        cameraUI.SetActive(false);
        photoFrame.SetActive(false);
        viewingPhoto = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("TakePicture"))
        {      
            if(!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect ((Screen.width/2) - 360, (Screen.height/2) - 360, 720, 720);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        ShowPhoto();

        byte[] bytes = screenCapture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameManager.Instance.activeSpot + ".png", bytes);

        if (photoHolder != null)
        {
            rend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            rend.material.mainTexture = screenCapture;
        }
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
    }
}
