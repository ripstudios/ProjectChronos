using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTimeShifter : MonoBehaviour
{
    public float fastSpeed;
    public float slowSpeed;

    private Animator anim;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (anim != null)
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
    }
}
