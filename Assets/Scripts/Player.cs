using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] pictureAreas;
    public GameObject fUI;
    public GameObject mainCam;
    public GameObject photoCam;
    public GameObject cameraManager;
    public float minDistance;
    public bool inRangeOfCameraArea;
    public bool cameraActive;
    private PlayerController pController;
    private Rigidbody rb;

    void Start()
    {
        pictureAreas = GameObject.FindGameObjectsWithTag("PhotoArea");
        fUI.SetActive(false);
        photoCam.SetActive(false);
        mainCam.SetActive(true);
        cameraManager.SetActive(false);
        cameraActive = false;
        pController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        foreach (GameObject area in pictureAreas)
        {
            if (Vector3.Distance(area.transform.position, transform.position) < minDistance)
            {
                //Debug.Log("In Range of Camera Area");
                inRangeOfCameraArea = true;
                if(mainCam.activeSelf)
                {
                    fUI.SetActive(true);
                }

                if(Input.GetKeyDown(KeyCode.F))
                {
                    if(photoCam.transform.position != area.transform.position)
                    {
                        photoCam.transform.position = new Vector3(area.transform.position.x, area.transform.position.y + 1.75f, area.transform.position.z);
                    }
                    mainCam.SetActive(false);
                    photoCam.SetActive(true);
                    cameraManager.SetActive(true);
                    fUI.SetActive(false);
                    rb.isKinematic = true;
                    pController.enabled = false;
                }
            }
            else
            {
                inRangeOfCameraArea = false;
                fUI.SetActive(false);
            }
        }
    }
}
