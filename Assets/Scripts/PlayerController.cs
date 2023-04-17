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
    public float gravity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Animator animator;
    private bool isFootSound;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask ground;

    [Header("Glider Mechanic")]
    public float glideSpeed;
    private bool isGliding;
    private GrapplingHook hook;

    private void Start()
    {
        hook = GetComponent<GrapplingHook>();
        controller.enabled = true;
    }

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

        if (direction.magnitude >= 0.1f)
        {
            float desiredRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // calculates angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, desiredRotation, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, desiredRotation, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            if (animator.GetBool("IsGliding"))
            {
                animator.SetBool("IsWalking", false);
                isFootSound = false;
                AudioManager.Instance.Stop("FootStepSound");
            }
            else
            {
                animator.SetBool("IsWalking", true);
                if (!isFootSound && isGrounded)
                {
                    isFootSound = true;
                    AudioManager.Instance.Play("FootStepSound");
                }
                else if (!isGrounded)
                {
                    isFootSound = false;
                    AudioManager.Instance.Stop("FootStepSound");
                }
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            isFootSound = false;
            AudioManager.Instance.Stop("FootStepSound");
        }

        if (isGrounded)
        {
            isGliding = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravity = 14f;
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * -gravity);
        }

        // glider
        if (Input.GetButtonDown("Glide") && !isGrounded) // && velocity.y < 0
        {
            animator.SetBool("IsWalking", false);
            isFootSound = false;
            AudioManager.Instance.Stop("FootStepSound");
            animator.SetBool("IsGliding", true);
            isGliding = true;
        }
        else if (isGliding && !isGrounded && velocity.y < 0)
        {
            // animator.SetBool("IsWalking", false);
            // isFootSound = false;
            // AudioManager.Instance.Stop("FootStepSound");
            // animator.SetBool("IsGliding", true);
            gravity = glideSpeed;
        }
        else if (isGrounded && !hook.hooked)
        {
            gravity = 14f;
            animator.SetBool("IsGliding", false);
        }

        if (Input.GetButtonUp("Glide") && isGliding)
        {
            isGliding = false;
            animator.SetBool("IsWalking", true);
            isFootSound = true;
            AudioManager.Instance.Play("FootStepSound");
            animator.SetBool("IsGliding", false);
            gravity = 14f;
        }

        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("isDeath"))
        {
            controller.enabled = false;
            GameManager.Instance.isDead = true;
        }

        if (collision.gameObject.CompareTag("portal"))
        {
            if (collision.gameObject.name == "ForestPortal")
            {
                GameManager.Instance.Portal("Redwood");
            }
            if (collision.gameObject.name == "CavePortal")
            {
                GameManager.Instance.Portal("Cave");
            }
            if (collision.gameObject.name == "CityPortal")
            {
                GameManager.Instance.Portal("City");
            }
        }
    }
}