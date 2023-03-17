using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] pictureAreas;
    public float minDistance;

    void Start()
    {
        pictureAreas = GameObject.FindGameObjectsWithTag("PhotoArea");
        
    }
    
    void Update()
    {
        

        foreach (GameObject area in pictureAreas)
        {
            if (Vector3.Distance(area.transform.position, transform.position) < minDistance)
            {
                Debug.Log("In Range");
                // Set UI indicator to true
            }
        }
    }
}
