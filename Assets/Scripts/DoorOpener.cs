using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;
    public float timeOpen = 3.0f;

    private Animator anim;
    private TriggeredDoor targetDoor;
    private bool unpressed = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetDoor = door.GetComponent<TriggeredDoor>();
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider c)
    {
        if (c.TryGetComponent(out ProtagControlScript protag))
        {
            anim.SetBool("pressed", true);
            if (unpressed)
            {
                targetDoor.btnsPressed++;
                unpressed = false;
                Invoke("CloseDoor", timeOpen);
            }
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
        unpressed = true;
        targetDoor.btnsPressed--;
    }
}
