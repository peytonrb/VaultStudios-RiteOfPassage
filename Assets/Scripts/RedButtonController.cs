using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonController : MonoBehaviour
{
    public GameObject console;

    // Start is called before the first frame update
    void Start()
    {
        console.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.win == true)
        {
            console.SetActive(true);
        }
    }
}
