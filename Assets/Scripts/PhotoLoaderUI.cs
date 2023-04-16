using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;

public class PhotoLoaderUI : MonoBehaviour
{
    private Image photo;
    private Texture2D tex;
    public Material mat;
    private byte[] imageData;
    public string whatImage;
    private bool rtPressed;
    void Update()
    {
        float rightTriggerValue = UnityEngine.Input.GetAxis("TakePicture");

        if (UnityEngine.Input.GetButtonDown("TakePicture"))
        {      
            //LoadPhoto();            
        }
        else if (rightTriggerValue == 1 && !rtPressed)
        {
            rtPressed = true;
            //LoadPhoto();
        }
        else if (rightTriggerValue < 1 && rtPressed)
        {
            rtPressed = false;
        }
        
    }   

    public void LoadPhoto()
    {
        photo = gameObject.GetComponent<Image>();
        photo.material = mat;
        if (File.Exists(Application.persistentDataPath + "/" + whatImage + ".png"))
        {
            imageData = File.ReadAllBytes(Application.persistentDataPath + "/" + whatImage + ".png");
            tex = new Texture2D(720, 720);
            tex.LoadImage(imageData);
            photo.material.mainTexture = tex;
        }
    }
}
