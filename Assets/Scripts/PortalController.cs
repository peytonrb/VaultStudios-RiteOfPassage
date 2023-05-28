// activates or deactivates the "return to hub" portals depending on player progress

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public GameObject portal;
    private string sceneName;


    void Start() 
    {
        // portal = GetComponent<GameObject>();
        portal.SetActive(false);
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (sceneName == "City")
        {
            Debug.Log("first if");
            Debug.Log(GameManager.Instance.cityW);
            if (GameManager.Instance.cityW)
            {
                Debug.Log("second if");
                portal.SetActive(true);
            }
        }

        if (sceneName == "redwood")
        {
            if (GameManager.Instance.redwoodW == true)
            {
                portal.SetActive(true);
            }   
        }

        if (sceneName == "Cave")
        {
            if (GameManager.Instance.caveW == true)
            {
                portal.SetActive(true);
            }   
        }
    }
}
