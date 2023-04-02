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
    private Animator animator;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask ground;

    [Header("Glider Mechanic")]
    public float glideSpeed;
    
    private void Update() 
    {
        animator = GetComponent<Animator>();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground); // is player on ground?

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
            animator.SetBool("IsWalking", true);
        }
        else{
            animator.SetBool("IsWalking", false);
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            gravity = 9.81f;
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * -gravity);
        }

        // glider
        if (Input.GetButtonDown("Glide") && !isGrounded) {
            gravity = glideSpeed;
            animator.SetBool("IsGliding", true);
        }
        else if(isGrounded){
            animator.SetBool("IsGliding", false);
        }

        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}