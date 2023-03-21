using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public float speed;
    // public float sensitivity;
    public Transform cam;
    private Vector3 velocity;
    private bool isGrounded;
    public float jumpHeight;
    private float gravity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask ground;

    [Header("Glider Mechanic")]
    public float glideSpeed;

    // private void Start() 
    // {
    //     controller = GetComponent<CharacterController>();    
    // }

    private void Update() 
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground); // is player on ground?
        // Debug.Log(isGrounded);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float desiredRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // calculates angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, desiredRotation, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, desiredRotation, 0f) * Vector3.forward; 
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

    //     Vector3 movementInput = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
    //     Vector3 movementDirection = movementInput.normalized;

    //     controller.Move(movementDirection * speed * Time.deltaTime);

    //     // player faces movement direction
    //     if (movementDirection != Vector3.zero) 
    //     {
    //         Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, sensitivity * Time.deltaTime);
    //     }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            gravity = -9.81f;
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        // if (Input.GetKeyDown(KeyCode.E) && !isGrounded) {
        //     gravity = -glideSpeed;
        // }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    // }
    }
}