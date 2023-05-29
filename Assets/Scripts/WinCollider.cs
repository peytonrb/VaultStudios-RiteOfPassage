using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour
{
    public GameObject fui;
    private bool withinRange;

    void Start()
    {
        fui.SetActive(false);
        withinRange = false;
    }

    void Update()
    {
        if (withinRange && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(4);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        withinRange = true;
        fui.SetActive(true);
    }

    public void OnTriggerExit(Collider collider)
    {
        withinRange = false;
        fui.SetActive(false);
    }
}
