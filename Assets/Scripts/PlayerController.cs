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
    public Vector3 velocity;
    private bool isGrounded;
    public float jumpHeight;
    public float gravity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Animator animator;
    private bool isFootSound;
    public float maxVelocityFalling;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask ground;
    public GameObject stepRayUpper;
    public GameObject stepRayLower;
    public float stepHeight;
    public float stepSmooth;

    [Header("Glider Mechanic")]
    public float glideSpeed;
    private bool isGliding;
    private GrapplingHook hook;

    [Header("Falling Animation")]
    private bool isFalling;

    [Header("Jumping Animation")]
    private bool isJumping;

    [Header("Death Animation")]
    private bool isDead;
    
    private void Start()
    {
        hook = GetComponent<GrapplingHook>();
        controller.enabled = true;
        isJumping = true;
        
    }

    private void Awake()
    {
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    private void Update()
    {
        bool previouslyGrounded = isGrounded;
        animator = GetComponent<Animator>();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground); // is player on ground?
        climbSteps();

        // FALL DAMAGE LOGIC
        if (!previouslyGrounded && isGrounded && (velocity.y < -maxVelocityFalling) && !GrapplingHook.fired)
        {
            controller.enabled = false;
            GameManager.Instance.isDead = true;
            animator.SetBool("isFalling", false);
        }
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 11f IS NOT A RANDOM VALUE, IT IS 2F MORE THAN THE MAX VELOCITY YOU GET WHEN JUMPING
        if (!isGrounded && velocity.y < -11f)
        {
            animator.SetBool("isFalling", true);
            
            
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
            animator.SetBool("isFalling", false);
            isGliding = false;
            if (GameManager.Instance.stam < GameManager.Instance.maxStam)
            {
                GameManager.Instance.gliding = false;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            gravity = 20f;
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * -gravity);
            StartCoroutine(finishJumping());
        }

        // glider
        if (Input.GetButtonDown("Glide") && !isGrounded && GameManager.Instance.stam > 0) // && velocity.y < 0
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("isFalling", false);
            isFootSound = false;
            AudioManager.Instance.Stop("FootStepSound");
            animator.SetBool("IsGliding", true);
            isGliding = true;
            
        }
        else if (isGliding && !isGrounded && velocity.y < 0)
        {
            gravity = glideSpeed;
            if (GameManager.Instance.stam > 0)
            {
                GameManager.Instance.gliding = true;
            }
            else
            {
                StopGliding();
            }
        }
        else if (isGrounded && !hook.hooked)
        {
            gravity = 20f;
            animator.SetBool("IsGliding", false);
        }

        if (Input.GetButtonUp("Glide") && isGliding)
        {
            StopGliding();
        }

        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator finishJumping()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsJumping", false);
    }

    void StopGliding()
    {
        isGliding = false;
        animator.SetBool("IsWalking", true);
        isFootSound = true;
        AudioManager.Instance.Play("FootStepSound");
        animator.SetBool("IsGliding", false);
        gravity = 20f;
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

    private void climbSteps()
    {
        RaycastHit hitLower;
        
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            Debug.Log("hit first");

            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                Debug.Log("hit second");
                GetComponent<Rigidbody>().position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
}