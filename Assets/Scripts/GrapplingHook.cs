using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("References")]
    public GameObject hook;
    public GameObject hookHolder;
    public LayerMask isGrappable;
    private Transform debugTransform;

    [Header("Variables")]
    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public static bool hooked;
    public static bool fired;
    public float maxDistance;
    private float currentDistance;

    void Update() {

        // firing the hook
        if (Input.GetKeyDown(KeyCode.F) && !fired) {
            fired = true;
        }

        if (fired) {
            // hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            Vector2 crosshair = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(crosshair);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, isGrappable)) {
                debugTransform.position = raycastHit.point;
            }

            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance) {
                returnHook();
            }
        }
    }

    private void returnHook() {
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}
