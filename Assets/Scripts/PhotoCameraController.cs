using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCameraController : MonoBehaviour
{
    private GameObject target;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    // bool lockedCursor = true; <-- commented bc variable is assigned but not being used

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        target = transform.parent.gameObject;
        // Getting Mouse Inputs
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Vertical Camera Rotation
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        // Horizontal Mouse Rotation
        target.transform.Rotate(Vector3.up * inputX);
        
    }
}
