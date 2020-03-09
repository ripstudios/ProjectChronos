using UnityEngine;

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

    private AIState aiState;
    private Animator anim;
    private GameObject newRifle;
    private GameObject muzzle;
    private float room2MinX;
    private float room2MinZ;
    private float room2MaxX;
    private float room2MaxZ;
    private int currWaypoint;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private DoorScript doorScript;

    void Awake()
    {
        anim = GetComponent<Animator>();
        newRifle = (GameObject)Instantiate(rifle);
        newRifle.transform.parent = rightGunBone;
        newRifle.transform.localPosition = Vector3.zero;
        newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
        muzzle = newRifle.transform.Find("Muzzle").gameObject;
        this.navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.doorScript = GameObject.Find("/Room 2/Door & Door Frame/door").GetComponent<DoorScript>();
        this.currWaypoint = -1;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.aiState = AIState.Patrol;
        this.room2MinX = GameObject.Find("/Room 2/wall/decorative_wall_3 (7)").transform.position.x;
        this.room2MinZ = GameObject.Find("/Room 2/wall/glass_panel_1 (1)").transform.position.z;
        this.room2MaxX = GameObject.Find("/Room 2/wall/decorative_wall_3 (3)").transform.position.x;
        this.room2MaxZ = GameObject.Find("/Room 2/Door & Door Frame").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeShift.Instance.fast)
        {
            anim.speed = fastSpeed;
        }
        else
        {
            anim.speed = slowSpeed;
        }

        bool isPlayerInRoom2 = ProtagControlScript.Instance.transform.position.x > this.room2MinX && ProtagControlScript.Instance.transform.position.x < this.room2MaxX && ProtagControlScript.Instance.transform.position.z > this.room2MinZ && ProtagControlScript.Instance.transform.position.z < this.room2MaxZ;
        switch (this.aiState)
        {
            case AIState.Patrol:

                if (isPlayerInRoom2)
                {
                    this.aiState = AIState.Attack;
                    this.navMeshAgent.stoppingDistance = 7;
                    Debug.Log("AIState changed to Attack");
                }
                else if (navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance < Mathf.Epsilon && !navMeshAgent.pathPending)
                {
                    SetNextWaypoint();
                }
                break;
            case AIState.Attack:
                if (!isPlayerInRoom2)
                {
                    this.aiState = AIState.Patrol;
                    this.navMeshAgent.stoppingDistance = 0;
                    this.anim.SetBool("firing", false);
                    this.SetNextWaypoint();
                    Debug.Log("AIState changed to Idle");
                }
                else if (Vector3.Distance(ProtagControlScript.Instance.transform.position, this.transform.position) >= this.navMeshAgent.stoppingDistance)
                {
                    this.navMeshAgent.SetDestination(GameObject.Find("/FGC_Male_Char_Adam").transform.position);
                }
                else
                {
                    Fire();
                }
                break;
            default:
                break;
        }
        float velocity = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        anim.SetFloat("vely", velocity);
    }
    
    private void OnDestroy()
    {
        doorScript.doorEnabled = true;
    }

    private void SetNextWaypoint()
    {
        if (this.waypoints == null || this.waypoints.Length == 0)
        {
            Debug.LogError("No waypoints specified");
        }
        else
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
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(ProtagControlScript.Instance.transform.position - this.transform.position), this.rotationSmoothSpeed * Time.deltaTime);
        anim.SetBool("firing", true);
    }

    void CreateBeam()
    {
        AudioSource blasterSource = newRifle.transform.Find("PewPew").gameObject.GetComponent<AudioSource>();
        blasterSource.PlayOneShot(blasterSource.clip);
        Instantiate(pewpew, muzzle.transform.position, muzzle.transform.rotation);
    }
}
