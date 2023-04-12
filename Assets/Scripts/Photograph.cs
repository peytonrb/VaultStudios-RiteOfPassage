using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Photograph : MonoBehaviour
{
    public GameObject[] pictureAreas;
    public GameObject photoSpot1;
    public bool spot1Captured;
    public GameObject photoSpot2;
    public bool spot2Captured;
    public GameObject photoSpot3;
    public bool spot3Captured;
    public GameObject fUI;
    public GameObject mainCam;
    public GameObject photoCam;
    public GameObject CameraStuff;
    public GameObject mainUI;
    public float minDistance;
    private PlayerController pController;
    private Rigidbody rb;
    private int score;

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
        if (Vector3.Distance(photoSpot1.transform.position, transform.position) < minDistance || Vector3.Distance(photoSpot2.transform.position, transform.position) < minDistance || Vector3.Distance(photoSpot3.transform.position, transform.position) < minDistance)
        {
            if (mainCam.activeSelf)
            {
                fUI.SetActive(true);
            }

            if (Vector3.Distance(photoSpot1.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 1;
                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    spot1Captured = true;
                    UpdateScore();
                }
            }
            else if (Vector3.Distance(photoSpot2.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 2;
                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    spot2Captured = true;
                    UpdateScore();
                }
            }
            else if (Vector3.Distance(photoSpot3.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 3;
                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    spot3Captured = true;
                    UpdateScore();
                }
            }
        }
        else
        {
            fUI.SetActive(false);
        }


        foreach (GameObject area in pictureAreas)
        {
            if (Vector3.Distance(area.transform.position, transform.position) < minDistance)
            {
                if (Input.GetButtonDown("Interact") & mainCam.activeSelf)
                {
                    if (photoCam.transform.position != area.transform.position)
                    {
                        photoCam.transform.parent = area.transform;
                        photoCam.transform.position = new Vector3(area.transform.position.x, area.transform.position.y + 1.75f, area.transform.position.z);
                    }
                    
                    ActivateCamera();
                }
                else if (Input.GetButtonDown("Interact") & !mainCam.activeSelf)
                {
                    DeactivateCamera();
                }
            }
        }

        if (SceneManager.GetActiveScene().name != "Cave")
        {
            spot1Captured = false;
            spot2Captured = false;
            spot3Captured = false;
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

    private void UpdateScore()
    {
        score = 0;
        if (spot1Captured)
        {
            score ++;
        }
        if(spot2Captured)
        {
            score ++;
        }
        if(spot3Captured)
        {
            score ++;
        }
        GameManager.Instance.photosCaptured = score;
    }
}
