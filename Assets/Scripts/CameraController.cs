using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSens = 3.0f;
    private float rotationY;
    private float rotationX;
    public Transform target;
    public float targetDistance = 3.0f;
    private Vector3 currentRotation;
    private Vector3 velocity = Vector3.zero;
    private float time = 0.2f;
    private Vector2 rotationXMinMax = new Vector2(-40, 40);

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        rotationY += mouseX;
        rotationX -= mouseY;

        // clamp rotations
        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref velocity, time);
        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * targetDistance;
    }
}