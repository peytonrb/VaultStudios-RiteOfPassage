using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "isGrappable")
        {
            player.GetComponent<GrapplingHook>().hooked = true;
        }
    }
}
