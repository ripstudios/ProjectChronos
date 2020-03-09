using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtagControlScript : MonoBehaviour
{
    public static ProtagControlScript Instance { get; private set; }

    public bool attacking;
    public float fastSpeed = 1.0f;
    public float slowSpeed = 1.0f;
    public Canvas gameOverMenu;
    public Slider timeShiftHud;

    private Animator anim;
    private int toggleSpeed;
    private bool InputMapToCircular = true;
    private bool isJumping = false;
    private CanvasGroup canvasGroup;
    private SwordCollector swordCollector;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        toggleSpeed = 9;
        attacking = false;
        canvasGroup = gameOverMenu.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        TimeShift.Instance.fast = true;
        TimeShift.Instance.hud = timeShiftHud;
        swordCollector = GetComponent<SwordCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateToggleSpeed();
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (InputMapToCircular)
        {
            // make coordinates circular
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);
        }

        float forwardSpeed = v * toggleSpeed / 3f;
        anim.SetFloat("VSpeed", forwardSpeed);

        // Work around for jump animation without root motion
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            this.transform.position = this.transform.position + (this.transform.forward * Time.deltaTime * forwardSpeed * 1.5f);
        }

        // TODO: have rotation also covered by root motion animations
        this.transform.rotation = this.transform.rotation * Quaternion.AngleAxis(h * Time.deltaTime * 200, Vector3.up);

        // Looks uber weird
        if (v == 0 && h != 0) { 
            if (h > 0)
            {
                anim.SetBool("TurningLeft", true);
            }
            else if (h < 0)
            {
                anim.SetBool("TurningRight", true);
            }
        } else
        {
            anim.SetBool("TurningLeft", false);
            anim.SetBool("TurningRight", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                isJumping = true;
                // Current jumping solution. Apply strong vertical force to rigidbody
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(0, 150, 0));

                anim.SetBool("Jumping", true);
            }
        } else
        {
            anim.SetBool("Jumping", false);
        }

        if (Input.GetButtonDown("Fire1")) {
            if (swordCollector.hasSword)
            {
                anim.Play("Attack");
                attacking = true;
                Invoke("DoneAttacking", 1f);
            }
        }

        // Player character no longer timeshifts
        //// TimeShift functionality
        //if (TimeShift.Instance.fast)
        //{
        //    anim.speed = fastSpeed;
        //}
        //else
        //{
        //    anim.speed = slowSpeed;
        //}

    }

    void DoneAttacking()
    {
        attacking = false;
    }

    void OnAnimatorMove()
    {
        // Currently only use root motion for walking, not jumping
        // TODO: use root motion animation for all movements
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            isJumping = false;
            this.transform.position = anim.rootPosition;
        } else
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            this.gameObject.transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            this.gameObject.transform.parent = null;
        }
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void UpdateToggleSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            toggleSpeed = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            toggleSpeed = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            toggleSpeed = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toggleSpeed = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            toggleSpeed = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            toggleSpeed = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            toggleSpeed = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            toggleSpeed = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            toggleSpeed = 9;
        }
    }

    public void GameOver()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
