using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredDoor : MonoBehaviour
{
    public int requiredBtns = 1;
    public int btnsPressed;
    public bool doorEnabled = true;
    public float fastSpeed = 1.0f;
    public float slowSpeed = 0.01f;
    public AudioSource open;
    public AudioClip fastOpen;
    public AudioClip slowOpen;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        btnsPressed = 0;
    }

    private void Update()
    {
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed;
            open.clip = fastOpen;
        }
        else
        {
            anim.speed = slowSpeed;
            open.clip = slowOpen;
        }
        if (btnsPressed == requiredBtns)
        {
            anim.SetBool("open", true);
        } else
        {
            anim.SetBool("open", false);
        }
    }
}
