using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public GameObject hook;
    public GameObject hookHolder;
    public LayerMask isGrappable;

    [Header("Variables")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public bool hooked;
    public static bool fired;
    public GameObject hookedObject;
    public float maxDistance;
    private float currentDistance;
    private bool grounded;

    void Update() {
        CharacterController charController = this.GetComponent<CharacterController>();
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();

        // firing the hook
        if (Input.GetKeyDown(KeyCode.F) && !fired) {
            fired = true;
        }

        // creates visual line (rope)
        if (fired) {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        // if fired but not hooked, hook continues to travel and returns to player
        if (fired && !hooked) {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance) {
                returnHook();
            }
        }

        // performs complete grapple
        if (hooked && fired) {
            hook.transform.parent = hookedObject.transform;
            charController.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, 
                                                    Time.deltaTime * playerTravelSpeed);
            charController.enabled = true;
            float distanceFromHook = Vector3.Distance(transform.position, hook.transform.position);
            rigidbody.useGravity = false;

            if (distanceFromHook < 1)
            {
                // player will climb up wall if close enough to top
                if (!grounded) {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 17f);
                }

                StartCoroutine("Climb");
                // returnHook();
            }
        } else {
            hook.transform.parent = hookHolder.transform;
            rigidbody.useGravity = true;
            charController.enabled = true;
        }
    }

    // initiates so player does not glitch into corners if the hook grabs the edge of a block
    private IEnumerator Climb() {
        yield return new WaitForSeconds(0.1f);
        returnHook();
    }

    // returns hook to original position
    private void returnHook() {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.positionCount = 0;
    }

    // helper method, performs ground check 
    private void isGrounded() {
        RaycastHit hit;
        float distance = 1f;
        Vector3 direction = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, direction, out hit, distance)) {
            grounded = true;
        } else {
            grounded = false;
        }
    }
}
