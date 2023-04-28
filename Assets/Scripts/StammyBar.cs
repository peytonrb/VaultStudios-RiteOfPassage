using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StammyBar : MonoBehaviour
{
    public Slider stamWheel;

    void Start()
    {
        GameManager.Instance.stam = GameManager.Instance.maxStam;
    }

    void Update()
    {
        stamWheel.value = GameManager.Instance.stam / GameManager.Instance.maxStam;

        

        if (GameManager.Instance.stam >= GameManager.Instance.maxStam)
        {
            stamWheel.gameObject.SetActive(false);
        }
        else 
        {
            stamWheel.gameObject.SetActive(true);
        }
    }
}
