using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagControlScript : MonoBehaviour
{
    public float fastSpeed;
    public float slowSpeed;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fastSpeed = 1.0f;
        slowSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("VSpeed", v);

        this.transform.position = this.transform.position + (v * this.transform.forward * Time.deltaTime * 2);
        this.transform.rotation = this.transform.rotation * Quaternion.AngleAxis(h * Time.deltaTime * 200, Vector3.up);

        // Looks uber weird
        if (v == 0) { 
            if (h > 0)
            {
                anim.SetBool("TurningLeft", true);
            }
            else if (h < 0)
            {
                anim.SetBool("TurningRight", true);
            }
            else
            {
                anim.SetBool("TurningLeft", false);
                anim.SetBool("TurningRight", false);
            }
        }
              
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping");
            anim.SetBool("Jumping", true);
            StartCoroutine(Wait(1f));
            anim.SetBool("Jumping", false);
        }

        if (Input.GetKey(KeyCode.Q)) {
            // TODO and check if he has sword
            anim.Play("Attack");
            Debug.Log("attack!");
        }

        // TimeShift functionality
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed;
        }
        else
        {
            anim.speed = slowSpeed;
        }

    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
