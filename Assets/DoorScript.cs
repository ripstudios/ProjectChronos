using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            anim.SetBool("open", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            anim.SetBool("open", false);
        }
    }
}
