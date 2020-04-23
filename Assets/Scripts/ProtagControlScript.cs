using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtagControlScript : MonoBehaviour
{
    public static ProtagControlScript Instance { get; private set; }

    public bool attacking;
    public bool dashing = false;
    public bool dead;
    public float fastSpeed = 1.0f;
    public float slowSpeed = 1.0f;
    public float dashDist = 5f;
    public Canvas gameOverMenu;
    public Canvas stageClearMenu;
    public Canvas pauseMenu;
    public Slider timeShiftHud;
    public GameObject ragdoll;
    public new GameObject camera;
    public bool godMode = false;
    public GameObject[] checkpoint;

    private Animator anim;
    private Rigidbody rb;
    private AudioSource swordSwing;
    private int toggleSpeed;
    private bool InputMapToCircular = true;
    private int isJumping = 0;
    private CanvasGroup gameOver;
    private CanvasGroup stageClear;
    private CanvasGroup pause;
    private SwordCollector swordCollector;
    private Vector3 dashEnd;
    private Vector3 origin;

    private float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        this.transform.position = checkpoint[TimeShift.Instance.stage].transform.position;
        this.transform.rotation = checkpoint[TimeShift.Instance.stage].transform.rotation;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        toggleSpeed = 9;
        attacking = false;
        dead = false;
        swordSwing = this.transform.Find("SwordSwing").GetComponent<AudioSource>();

        gameOver = gameOverMenu.GetComponent<CanvasGroup>();
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;
        gameOver.alpha = 0f;

        stageClear = stageClearMenu.GetComponent<CanvasGroup>();
        stageClear.interactable = false;
        stageClear.blocksRaycasts = false;
        stageClear.alpha = 0f;

        pause = pauseMenu.GetComponent<CanvasGroup>();
        pause.interactable = false;
        pause.blocksRaycasts = false;
        pause.alpha = 0f;

        Time.timeScale = 1f;
        TimeShift.Instance.fast = true;
        TimeShift.Instance.hud = timeShiftHud;
        swordCollector = GetComponent<SwordCollector>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateToggleSpeed();
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (InputMapToCircular)
        {
            // make coordinates circular
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);
        }

        float forwardSpeed = v * toggleSpeed / 3f;
        float strafeSpeed = h * toggleSpeed;

        anim.SetFloat("VSpeed", forwardSpeed);
        anim.SetFloat("HSpeed", 0);

        if (h != 0 && v == 0) {
            anim.SetFloat("HSpeed", strafeSpeed);
        }

        // Work around for jump animation without root motion
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            this.transform.position = this.transform.position + (this.transform.forward * Time.deltaTime * forwardSpeed * 1.5f);
        }

        // TODO: have rotation also covered by root motion animations
        // TODO: Also allow rotation with keys

        // if (h != 0) {
        //     h = 0;
        // } else {
        //     float lastMouseX = mouseX;
        //     mouseX += Input.GetAxis("Mouse X");
  
        //     if (lastMouseX != mouseX) {
        //         h = mouseX;
        //     }
        // }



        // Looks uber weird
        // if (v == 0 && h != 0) { 
        //     if (h < 0)
        //     {
        //         anim.SetBool("TurningLeft", true);
        //         if (Input.GetKey(KeyCode.A))
        //           transform.Rotate(-Vector3.up * 120.0f * Time.deltaTime);
      
                
        //     }
        //     else if (h > 0)
        //     {
        //         anim.SetBool("TurningRight", true);
        //         if (Input.GetKey(KeyCode.D))
        //             this.transform.Rotate(Vector3.up * 120.0f * Time.deltaTime);
        //     }
        // } else
        // {
        //     anim.SetBool("TurningLeft", false);
        //     anim.SetBool("TurningRight", false);
        // }

        if (!gameOver.interactable && !stageClear.interactable && !pause.interactable)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (isJumping < 2)
                {
                    isJumping++;
                    // Current jumping solution. Apply strong vertical force to rigidbody
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.AddForce(new Vector3(0, 150, 0));

                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                anim.SetBool("Jumping", false);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (swordCollector.hasSword)
                {
                    swordSwing.Play();
                    anim.Play("Attack");
                    attacking = true;
                    Invoke("DoneAttacking", 1f);
                }
            }

            if (Input.GetButtonDown("Fire3"))
            {
                origin = this.transform.position;
                anim.SetBool("dash", true);
                swordSwing.Play();
                anim.Play("Attack");
                attacking = true;
                dashing = true;
            }
        }

        if (dashing)
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, dashDist))
            {
                if (hit.distance < 0.7)
                {
                    // Debug.Log("WALL");
                    DoneDashing();
                }
            } else
            {
                if ((this.transform.position - origin).magnitude >= dashDist)
                {
                    DoneDashing();
                }
                if (isJumping > 0)
                {
                    this.transform.position = this.transform.position + this.transform.forward * 1.5f;
                }
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

    void DoneDashing()
    {
        anim.SetBool("dash", false);
        attacking = false;
        dashing = false;
    }

    void OnAnimatorMove()
    {
        // Currently only use root motion for walking, not jumping
        // TODO: use root motion animation for all movements
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            this.transform.position = anim.rootPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            this.gameObject.transform.parent = other.transform;
        }
        isJumping = 0;
        DoneDashing();
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
        if (!godMode)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            camera.GetComponent<CameraFollow>().enabled = false;

            Transform[] ragdollJoints = ragdoll.GetComponentsInChildren<Transform>();
            Transform[] currentJoints = GetComponentsInChildren<Transform>();

            for (int i = 0; i < ragdollJoints.Length; i++)
            {
                for (int q = 0; q < currentJoints.Length; q++)
                {
                    if (currentJoints[q].name.CompareTo(ragdollJoints[i].name) == 0)
                    {
                        ragdollJoints[i].position = currentJoints[q].position;
                        ragdollJoints[i].rotation = currentJoints[q].rotation;
                        break;
                    }
                }
            }
            dead = true;
            gameOver.interactable = true;
            gameOver.blocksRaycasts = true;
            gameOver.alpha = 1f;
            this.gameObject.SetActive(false);
            Invoke("EndTime", 0.5f);
        }
        
    }

    public void StageClear()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        camera.GetComponent<CameraFollow>().enabled = false;

        stageClear.interactable = true;
        stageClear.blocksRaycasts = true;
        stageClear.alpha = 1f;
        Invoke("EndTime", 0);
    }

    void EndTime()
    {
        Time.timeScale = 0;
    }
}
