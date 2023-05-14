// adds functionality to the grappling hook mechanic, including the rendering and physics

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public GameObject hook;
    public GameObject hookHolder;
    public LayerMask isGrappable;
    public Camera cam;
    private PlayerController player;

    [Header("Variables")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public bool hooked;
    public static bool fired;
    public GameObject hookedObject;
    public float maxDistance;
    public float hookProximity; // how close to hook will player release grapple and drop to ground
    private float currentDistance;
    private bool grounded;
    private Animator animator;

    void Update()
    {
        animator = GetComponent<Animator>();
        CharacterController charController = this.GetComponent<CharacterController>();
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        player = this.GetComponent<PlayerController>();

        // firing the hook
        if (Input.GetButtonDown("Grapple") && !fired)
        {
            fired = true;
            AudioManager.Instance.Play("GrappleSound");
        }

        // creates visual line (rope)
        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        // if fired but not hooked, hook continues to travel and/or returns to player
        if (fired && !hooked)
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(new Vector3(x, y, 0));

            if (Physics.Raycast(ray, out hit, maxDistance, 9))
            {
                if (hit.collider.tag == "isGrappable")
                {
                    transform.LookAt(hit.point); // in case player is backwards when fire
                    hook.transform.LookAt(hit.point);
                    hook.transform.position = Vector3.MoveTowards(hook.transform.position, hit.point, maxDistance);
                    // hook.transform.Translate(ray.direction * hookTravelSpeed * Time.deltaTime);
                    currentDistance = Vector3.Distance(transform.position, hook.transform.position);

                    if (currentDistance >= maxDistance)
                    {
                        returnHook();
                    }
                }
            }
            else
            {
                returnHook();
            }
        }

        // performs complete grapple
        if (hooked && fired)
        {
            hook.transform.parent = hookedObject.transform;
            charController.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position,
                                                    Time.deltaTime * playerTravelSpeed);
            charController.enabled = true;
            float distanceFromHook = Vector3.Distance(transform.position, hook.transform.position);
            rigidbody.useGravity = false;
            animator.SetBool("IsGrappling", true);

            if (distanceFromHook < hookProximity)
            {
                // StartCoroutine("Climb");
                returnHook();
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            rigidbody.useGravity = true;
            charController.enabled = true;
            animator.SetBool("IsGrappling", false);
        }
    }

    // initiates so player does not glitch into corners if the hook grabs the edge of a block
    private IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        returnHook();
    }

    // returns hook to original position
    private void returnHook()
    {
        player.velocity = new Vector3(0, -2, 0);
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;
    }

    // helper method, performs ground check 
    private void isGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 direction = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
