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

        // firing the hook
        if (Input.GetKeyDown(KeyCode.F) && !fired) {
            fired = true;
        }

        if (fired) {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired && !hooked) {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance) {
                returnHook();
            }
        }

        if (hooked && fired) {
            hook.transform.parent = hookedObject.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, 
                                                    Time.deltaTime * playerTravelSpeed);
            float distanceFromHook = Vector3.Distance(transform.position, hook.transform.position);
            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceFromHook < 1)
            {
                // climb up wall if close enough to top
                if (!grounded) {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 17f);
                }

                StartCoroutine("Climb");
                // returnHook();
            }
        } else {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private IEnumerator Climb() {
        yield return new WaitForSeconds(0.1f);
        returnHook();
    }

    private void returnHook() {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }

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
