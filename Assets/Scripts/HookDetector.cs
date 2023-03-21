using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    // determines if hook is attached to a surface
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "isGrappable")
        {
            player.GetComponent<GrapplingHook>().hooked = true;
            player.GetComponent<GrapplingHook>().hookedObject = collision.gameObject;
        }
    }
}
