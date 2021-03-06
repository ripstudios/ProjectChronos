﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorEnabled = true;
    public bool closed = false;
    public float fastSpeed = 1.0f;
    public float slowSpeed = 0.5f;
    public AudioSource open;
    public AudioClip fastOpen;
    public AudioClip slowOpen;

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
            open.clip = fastOpen;
        }
        else
        {
            anim.speed = slowSpeed;
            open.clip = slowOpen;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!TimeShift.Instance.respawned || !closed)
        {
            if (this.doorEnabled && other.CompareTag("Player"))
            {
                anim.SetBool("open", true);
                open.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!TimeShift.Instance.respawned || !closed)
        {
            if (this.doorEnabled && other.CompareTag("Player"))
            {
                anim.SetBool("open", false);
            }
        }
    }
}
