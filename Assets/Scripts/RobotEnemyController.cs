using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyController : MonoBehaviour
{
    public Transform rightGunBone;
    public GameObject rifle;
    public GameObject pewpew;
    public float fastSpeed = 3.0f;
    public float slowSpeed = 0.1f;

    private Animator anim;
    private GameObject newRifle;
    private GameObject muzzle;

    void Awake()
    {
        anim = GetComponent<Animator>();
        newRifle = (GameObject)Instantiate(rifle);
        newRifle.transform.parent = rightGunBone;
        newRifle.transform.localPosition = Vector3.zero;
        newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
        muzzle = newRifle.transform.GetChild(0).gameObject;
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

        if (Vector3.Distance(ProtagControlScript.Instance.transform.position, this.transform.position) < 10)
        {
            Fire();
        }
    }

    void Fire()
    {
        //TODO Interpolate with animation
        this.transform.LookAt(ProtagControlScript.Instance.transform);
        anim.SetBool("firing", true);
    }

    void CreateBeam()
    {
        Instantiate(pewpew, muzzle.transform.position, muzzle.transform.rotation);
    }
}
