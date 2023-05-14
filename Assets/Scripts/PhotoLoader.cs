// loads player photo into UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotoLoader : MonoBehaviour
{
    private Renderer rend;
    private Texture2D tex;
    private byte[] imageData;
    public string whatImage;
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        if (File.Exists(Application.persistentDataPath + "/" + whatImage + ".png"))
        {
            imageData = File.ReadAllBytes(Application.persistentDataPath + "/" + whatImage + ".png");
            tex = new Texture2D(720, 720);
            tex.LoadImage(imageData);
            rend.material.mainTexture = tex;
        }
    }   
}
