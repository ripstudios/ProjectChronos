﻿using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class RobotEnemyController : MonoBehaviour
{
    private enum AIState
    {
        Patrol,
        Attack
    };

    public Transform rightGunBone;
    public GameObject rifle;
    public GameObject pewpew;
    public GameObject[] waypoints;
    public float fastSpeed;
    public float slowSpeed;
    public float rotationSmoothSpeed;
    public GameObject guardAreaMinimumX;
    public GameObject guardAreaMinimumZ;
    public GameObject guardAreaMaximumX;
    public GameObject guardAreaMaximumZ;
    public GameObject guardDoor;

    private AIState aiState;
    private Animator anim;
    private GameObject newRifle;
    private GameObject muzzle;
    private int currWaypoint;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private DoorScript doorScript;
    private float speed;
    private float speedMultiplier;

    void Awake()
    {
        anim = GetComponent<Animator>();
        newRifle = (GameObject)Instantiate(rifle);
        newRifle.transform.parent = rightGunBone;
        newRifle.transform.localPosition = Vector3.zero;
        newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
        muzzle = newRifle.transform.Find("Muzzle").gameObject;
        this.navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (guardDoor != null)
        {
            this.doorScript = guardDoor.GetComponent<DoorScript>();
        }
        this.currWaypoint = -1;
        this.speed = this.navMeshAgent.speed;
        this.speedMultiplier = 1.0f;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.aiState = AIState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed * this.speedMultiplier;
            this.navMeshAgent.speed = this.speed * fastSpeed * this.speedMultiplier;
        }
        else
        {
            anim.speed = slowSpeed * this.speedMultiplier;
            this.navMeshAgent.speed = this.speed * slowSpeed * this.speedMultiplier;
        }

        bool isPlayerInRoom = ProtagControlScript.Instance.transform.position.x > this.guardAreaMinimumX.transform.position.x && ProtagControlScript.Instance.transform.position.x < this.guardAreaMaximumX.transform.position.x && ProtagControlScript.Instance.transform.position.z > this.guardAreaMinimumZ.transform.position.z && ProtagControlScript.Instance.transform.position.z < this.guardAreaMaximumZ.transform.position.z;
        switch (this.aiState)
        {
            case AIState.Patrol:
                if (isPlayerInRoom)
                {
                    this.aiState = AIState.Attack;
                    this.navMeshAgent.stoppingDistance = 8;
                    this.speedMultiplier = 3.0f;
                    Debug.Log("AIState changed to Attack");
                }
                else if (navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance < Mathf.Epsilon && !navMeshAgent.pathPending)
                {
                    SetNextWaypoint();
                }
                break;
            case AIState.Attack:
                if (!isPlayerInRoom)
                {
                    this.aiState = AIState.Patrol;
                    this.navMeshAgent.isStopped = true;
                    this.navMeshAgent.stoppingDistance = 0;
                    this.anim.SetBool("firing", false);
                    this.speedMultiplier = 1.0f;
                    this.SetNextWaypoint();
                    Debug.Log("AIState changed to Patrol");
                }
                else if (Vector3.Distance(ProtagControlScript.Instance.transform.position, this.transform.position) >= this.navMeshAgent.stoppingDistance)
                {
                    anim.SetBool("firing", false);
                    this.navMeshAgent.SetDestination(ProtagControlScript.Instance.transform.position);
                }
                else
                {
                    Fire();
                }
                break;
            default:
                break;
        }
        float velocity = navMeshAgent.velocity.magnitude / 3.0f / (TimeShift.Instance.fast ? fastSpeed : slowSpeed);
        anim.SetFloat("vely", velocity);
    }
    
    private void OnDestroy()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("Robot Death");
        }
        if (doorScript != null)
        {
            doorScript.doorEnabled = true;
        }
    }

    private void SetNextWaypoint()
    {
        if (this.waypoints != null && this.waypoints.Length != 0)
        {
            if (this.currWaypoint == this.waypoints.Length - 1)
            {
                this.currWaypoint = 0;
            }
            else
            {
                this.currWaypoint += 1;
            }
            this.navMeshAgent.SetDestination(this.waypoints[this.currWaypoint].transform.position);
        }
    }

    void Fire()
    {
        if (TimeShift.Instance.fast)
        {
            this.transform.LookAt(ProtagControlScript.Instance.transform.position);
        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(ProtagControlScript.Instance.transform.position - this.transform.position), this.rotationSmoothSpeed * Time.deltaTime);
        }
        anim.SetBool("firing", true);
    }

    void CreateBeam()
    {
        AudioSource blasterSource = newRifle.transform.Find("PewPew").gameObject.GetComponent<AudioSource>();
        blasterSource.PlayOneShot(blasterSource.clip);
        Instantiate(pewpew, muzzle.transform.position, muzzle.transform.rotation);
    }
}
