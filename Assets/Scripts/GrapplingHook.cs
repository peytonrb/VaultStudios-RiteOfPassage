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
    public float maxDistance;
    private float currentDistance;

    void Update() {

        // firing the hook
        if (Input.GetKeyDown(KeyCode.F) && !fired) {
            fired = true;
        }

        if (fired && !hooked) {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance) {
                returnHook();
            }
        }

        if (hooked) {
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceFromHook = Vector3.Distance(transform.position, hook.transform.position);
            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceFromHook < 1)
            {
                returnHook();
            }
        } else {
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void returnHook() {
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}
