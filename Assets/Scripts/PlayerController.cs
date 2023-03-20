using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float speed;
    public float sensitivity;
    public new Camera camera;
    private Vector3 velocity;
    private bool isGrounded;
    public float jumpHeight;
    private float gravity = -9.81f;

    [Header("Glider Mechanic")]
    public float glideSpeed;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();    
    }

    private void Update() 
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        controller.Move(movementDirection * speed * Time.deltaTime);

        // player faces movement direction
        if (movementDirection != Vector3.zero) 
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, sensitivity * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        } 

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}