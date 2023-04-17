using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    public GameObject cameraStuff;
    public GameObject mainUI;
    public GameObject polarUI1;
    public GameObject polarUI2;
    public GameObject polarUI3;
    public GameObject thirdPersCam;
    public float minDistance;
    private CinemachineFreeLook cmVirCam;
    private PlayerController pController;
    private PhotoCameraController pCController;
    private PhotoCapture camManager;
    private Rigidbody rb;
    private PhotoBounds photoBounds;
    private int score;

    void Start()
    {
        pController = GetComponent<PlayerController>();
        pCController = photoCam.GetComponent<PhotoCameraController>();
        camManager = cameraStuff.GetComponentInChildren<PhotoCapture>();
        cmVirCam = thirdPersCam.GetComponent<CinemachineFreeLook>();
        pictureAreas = GameObject.FindGameObjectsWithTag("PhotoArea");
        fUI.SetActive(false);
        photoCam.SetActive(false);
        mainCam.SetActive(true);
        cameraStuff.SetActive(false);
        rb = GetComponent<Rigidbody>();
        mainUI.SetActive(true);
    }
    
    void Update()
    {
        if (Vector3.Distance(photoSpot1.transform.position, transform.position) < minDistance || Vector3.Distance(photoSpot2.transform.position, transform.position) < minDistance || Vector3.Distance(photoSpot3.transform.position, transform.position) < minDistance)
        {
            if (mainCam.activeSelf && GameManager.Instance.currentScene != "Hub")
            {
                fUI.SetActive(true);
            }
            else if (mainCam.activeSelf && Vector3.Distance(photoSpot1.transform.position, transform.position) < minDistance)
            {
                fUI.SetActive(true);
            }
            else
            {
                fUI.SetActive(false);
            }

            if (Vector3.Distance(photoSpot1.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 1;
                if (camManager.newPhoto)
                {
                    camManager.newPhoto = false;
                    //polarUI1.GetComponent<PhotoLoaderUI>().LoadPhoto();
                }

                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    if (photoBounds.inRange(pictureAreas[0].transform.eulerAngles.y, photoCam.transform.eulerAngles.x) && !camManager.viewingPhoto)
                    {
                        GameManager.Instance.savePhoto = true;
                        spot1Captured = true;
                        UpdateScore();
                    }
                }
            }
            else if (Vector3.Distance(photoSpot2.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 2;
                
                if (camManager.newPhoto)
                {
                    camManager.newPhoto = false;
                    //polarUI2.GetComponent<PhotoLoaderUI>().LoadPhoto();
                }

                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    if (photoBounds.inRange(pictureAreas[1].transform.eulerAngles.y, photoCam.transform.eulerAngles.x) && !camManager.viewingPhoto)
                    {
                        GameManager.Instance.savePhoto = true;
                        spot2Captured = true;
                        UpdateScore();
                    }
                }
            }
            else if (Vector3.Distance(photoSpot3.transform.position, transform.position) < minDistance)
            {
                GameManager.Instance.activeSpot = 3;
                
                if (camManager.newPhoto)
                {
                    camManager.newPhoto = false;
                    //polarUI3.GetComponent<PhotoLoaderUI>().LoadPhoto();
                }
                
                if (Input.GetButtonDown("TakePicture") || Input.GetAxis("TakePicture") == 1)
                {
                    if (photoBounds.inRange(pictureAreas[2].transform.eulerAngles.y, photoCam.transform.eulerAngles.x) && !camManager.viewingPhoto)
                    {
                        GameManager.Instance.savePhoto = true;
                        spot3Captured = true;
                        UpdateScore();
                    }
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
                photoBounds = area.GetComponent<PhotoBounds>();
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

        if (SceneManager.GetActiveScene().name == "Hub")
        {
            spot1Captured = false;
            spot2Captured = false;
            spot3Captured = false;
        }

        if (GameManager.Instance.PauseMenu != null)
        {
            if (GameManager.Instance.PauseMenu.activeSelf)
            {
                pCController.enabled = false;
                thirdPersCam.SetActive(false);
                mainUI.SetActive(false);
                camManager.enabled = false;
            }
            else if (!GameManager.Instance.PauseMenu.activeSelf && !photoCam.activeSelf)
            {
                thirdPersCam.SetActive(true);
                mainUI.SetActive(true);
            }
            else if (photoCam.activeSelf)
            {
                pCController.enabled = true;
                camManager.enabled = true;
                fUI.SetActive(false);
                mainUI.SetActive(true);
            }
        }
    }

    private void ActivateCamera()
    {
        mainCam.SetActive(false);
        photoCam.SetActive(true);
        cameraStuff.SetActive(true);
        rb.isKinematic = true;
        pController.enabled = false;
        mainUI.SetActive(false);
    }

    private void DeactivateCamera()
    {
        mainCam.SetActive(true);
        photoCam.SetActive(false);
        cameraStuff.SetActive(false);
        rb.isKinematic = false;
        pController.enabled = true;
        mainUI.SetActive(true);
    }

    private void UpdateScore()
    {
        score = 0;
        if (spot1Captured)
        {
            //polarUI1.GetComponent<PhotoLoaderUI>().LoadPhoto();
            score ++;
        }
        if(spot2Captured)
        {
            //polarUI2.GetComponent<PhotoLoaderUI>().LoadPhoto();
            score ++;
        }
        if(spot3Captured)
        {
            //polarUI3.GetComponent<PhotoLoaderUI>().LoadPhoto();
            score ++;
        }
        GameManager.Instance.photosCaptured = score;
    }
}
