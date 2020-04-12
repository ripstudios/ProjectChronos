using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;

    private Animator anim;
    private Animator doorAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        doorAnim = door.GetComponent<Animator>();
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider c)
    {
        if (c.TryGetComponent(out ProtagControlScript protag))
        {
            anim.SetBool("pressed", true);
            doorAnim.SetBool("open", true);
            Invoke("CloseDoor", 3.0f);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.TryGetComponent(out ProtagControlScript protag))
        {
            anim.SetBool("pressed", false);
        }
    }

    void CloseDoor()
    {
        doorAnim.SetBool("open", false);
    }
}
