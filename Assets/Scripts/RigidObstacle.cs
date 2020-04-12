using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidObstacle : MonoBehaviour
{
    public float fastSpeed = 1.0f;
    public float slowSpeed = 0.2f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed;
        } else
        {
            anim.speed = slowSpeed;
        }
    }
}
