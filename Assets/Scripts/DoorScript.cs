using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float fastSpeed = 1.0f;
    public float slowSpeed = 0.1f;

    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed;
        }
        else
        {
            anim.speed = slowSpeed;
        }
    }

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
