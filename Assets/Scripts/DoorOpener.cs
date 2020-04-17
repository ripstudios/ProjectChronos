using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;
    public float timeOpen = 3.0f;
    public Material defaultColor;
    public Material pressedColor;

    private Animator anim;
    private TriggeredDoor targetDoor;
    private Renderer rend;
    private bool unpressed = true;
    private float pressedCountdown = 0;

    private readonly int buttonTimer = 200;
    private readonly int fastIncrement = 10;
    private readonly int slowIncrement = 1;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetDoor = door.GetComponent<TriggeredDoor>();
        rend = GetComponent<Renderer>();
        rend.material = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressedCountdown > 0)
        {
            float increment;
            if (TimeShift.Instance.fast)
            {
                increment = fastIncrement;
            } else
            {
                increment = slowIncrement;
            }
            pressedCountdown -= increment;

            if (pressedCountdown <= 0)
            {
                UnpressButton();
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.TryGetComponent(out ProtagControlScript protag))
        {
            anim.SetBool("pressed", true);
            if (unpressed)
            {
                rend.material = pressedColor;
                targetDoor.btnsPressed++;
                unpressed = false;
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.TryGetComponent(out ProtagControlScript protag))
        {
            anim.SetBool("pressed", false);
            if (!unpressed)
            {
                pressedCountdown = buttonTimer;
            }
        }
    }

    void UnpressButton()
    {
        rend.material = defaultColor;
        unpressed = true;
        targetDoor.btnsPressed--;
    }
}
