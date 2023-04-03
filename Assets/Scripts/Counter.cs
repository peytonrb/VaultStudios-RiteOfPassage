using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TMP_Text counter;
    void Update()
    {
        counter.text = GameManager.Instance.photosCaptured + "/3";
    }
}
