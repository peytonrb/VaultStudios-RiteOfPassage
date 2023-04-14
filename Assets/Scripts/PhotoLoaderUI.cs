using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;

public class PhotoLoaderUI : MonoBehaviour
{
    private Image photo;
    private Texture2D tex;
    private byte[] imageData;
    public string whatImage;
    private bool rtPressed;
    void Update()
    {
        float rightTriggerValue = UnityEngine.Input.GetAxis("TakePicture");

        if (UnityEngine.Input.GetButtonDown("TakePicture"))
        {      
            photo = gameObject.GetComponent<Image>();
            photo.material = new Material(Shader.Find("Unlit/Texture"));
            if (File.Exists(Application.persistentDataPath + "/" + whatImage + ".png"))
            {
                imageData = File.ReadAllBytes(Application.persistentDataPath + "/" + whatImage + ".png");
                tex = new Texture2D(720, 720);
                tex.LoadImage(imageData);
                photo.material.mainTexture = tex;
            }
        }
        else if (rightTriggerValue == 1 && !rtPressed)
        {
            rtPressed = true;
            photo = gameObject.GetComponent<Image>();
            photo.material = new Material(Shader.Find("Unlit/Texture"));
            if (File.Exists(Application.persistentDataPath + "/" + whatImage + ".png"))
            {
                imageData = File.ReadAllBytes(Application.persistentDataPath + "/" + whatImage + ".png");
                tex = new Texture2D(720, 720);
                tex.LoadImage(imageData);
                photo.material.mainTexture = tex;
            }
        }
        else if (rightTriggerValue < 1 && rtPressed)
        {
            rtPressed = false;
        }
    }   
}
