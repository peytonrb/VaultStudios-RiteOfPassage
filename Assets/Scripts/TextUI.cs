using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TMP_Text counter;
    public string[] objText;

    private void Update()
    {
        if (GameManager.Instance.activeSpot == 1 && !GameManager.Instance.spot1)
        {
            counter.text = objText[1];
        }
        else if (GameManager.Instance.activeSpot == 2 && !GameManager.Instance.spot2)
        {
            counter.text = objText[2];
        }
        else if (GameManager.Instance.activeSpot == 3 && !GameManager.Instance.spot3)
        {
            counter.text = objText[3];
        }
        else 
        {
            OtherText();
        }
    }

    private void OtherText()
    {
        if(GameManager.Instance.activeSpot == 0)
        {
            counter.text = objText[0];
        }
        else if (!GameManager.Instance.spot1)
        {
            counter.text = objText[1];
        }
        else if (!GameManager.Instance.spot2)
        {
            counter.text = objText[2];
        }
        else if (!GameManager.Instance.spot3)
        {
            counter.text = objText[3];
        }
        else 
        {
            counter.text = "Press ESC and return to the Hub";
        }
    }
}
