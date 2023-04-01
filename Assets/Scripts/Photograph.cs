using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : MonoBehaviour
{
    public GameObject[] pictureAreas;
    public GameObject fUI;
    public GameObject mainCam;
    public GameObject photoCam;
    public GameObject CameraStuff;
    public GameObject mainUI;
    public float minDistance;
    private PlayerController pController;
    private Rigidbody rb;

    void Start()
    {
        pictureAreas = GameObject.FindGameObjectsWithTag("PhotoArea");
        fUI.SetActive(false);
        photoCam.SetActive(false);
        mainCam.SetActive(true);
        CameraStuff.SetActive(false);
        pController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        mainUI.SetActive(true);
    }
    
    void Update()
    {
        foreach (GameObject area in pictureAreas)
        {
            if (Vector3.Distance(area.transform.position, transform.position) < minDistance)
            {
                //Debug.Log("In Range of Camera Area");
                if (mainCam.activeSelf)
                {
                    fUI.SetActive(true);
                }
                else
                {
                    fUI.SetActive(false);
                }

                if (Input.GetButtonDown("Interact") & mainCam.activeSelf)
                {
                    if (photoCam.transform.position != area.transform.position)
                    {
                        photoCam.transform.position = new Vector3(area.transform.position.x, area.transform.position.y + 1.75f, area.transform.position.z);
                    }
                    
                    ActivateCamera();
                }
                else if (Input.GetButtonDown("Interact") & !mainCam.activeSelf)
                {
                    DeactivateCamera();
                }
            }
            else
            {
                fUI.SetActive(false);
            }
        }
    }

    private void ActivateCamera()
    {
        mainCam.SetActive(false);
        photoCam.SetActive(true);
        CameraStuff.SetActive(true);
        rb.isKinematic = true;
        pController.enabled = false;
        mainUI.SetActive(false);
    }

    private void DeactivateCamera()
    {
        mainCam.SetActive(true);
        photoCam.SetActive(false);
        CameraStuff.SetActive(false);
        rb.isKinematic = false;
        pController.enabled = true;
        mainUI.SetActive(true);
    }
}
