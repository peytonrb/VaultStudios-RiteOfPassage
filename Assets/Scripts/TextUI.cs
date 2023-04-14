using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TMP_Text counter;
    void Update()
    {
        if (GameManager.Instance.activeSpot == 1)
        {
            counter.text = "Capture a photo of the Mineshaft";
        }
        else if (GameManager.Instance.activeSpot == 2)
        {
            counter.text = "Capture a photo of the Giant Mushrooms";
        }
        else if (GameManager.Instance.activeSpot == 3)
        {
            counter.text = "Capture a photo of the Giant Crystal";
        }
        else
        {
            counter.text = "Take a photo at each of the three yellow rings";
        }
    }
}
