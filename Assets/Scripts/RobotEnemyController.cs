using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class RobotEnemyController : MonoBehaviour
{
    private enum AIState
    {
        Idle,
        Attack
    };

    public Transform rightGunBone;
    public GameObject rifle;
    public GameObject pewpew;
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
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private DoorScript doorScript;

    void Awake()
    {
        anim = GetComponent<Animator>();
        newRifle = (GameObject)Instantiate(rifle);
        newRifle.transform.parent = rightGunBone;
        newRifle.transform.localPosition = Vector3.zero;
        newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
        muzzle = newRifle.transform.GetChild(0).gameObject;
        this.navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.doorScript = GameObject.Find("/Room 2/Door & Door Frame/door").GetComponent<DoorScript>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.aiState = AIState.Idle;
        this.room2MinX = GameObject.Find("/Room 2/wall/decorative_wall_3 (7)").transform.position.x;
        this.room2MinZ = GameObject.Find("/Room 2/wall/glass_panel_1 (1)").transform.position.z;
        this.room2MaxX = GameObject.Find("/Room 2/wall/decorative_wall_3 (3)").transform.position.x;
        this.room2MaxZ = GameObject.Find("/Room 2/Door & Door Frame").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        Debug.Log(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
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
            case AIState.Idle:
                if (isPlayerInRoom2)
                {
                    this.aiState = AIState.Attack;
                    Debug.Log("AIState changed to Attack");
                }
                break;
            case AIState.Attack:
                if (!isPlayerInRoom2)
                {
                    this.aiState = AIState.Idle;
                    this.anim.SetBool("firing", false);
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
    }
    
    private void OnDestroy()
    {
        doorScript.enabled = true;
    }

    void Fire()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(ProtagControlScript.Instance.transform.position - this.transform.position), this.rotationSmoothSpeed * Time.deltaTime);
        anim.SetBool("firing", true);
    }

    void CreateBeam()
    {
        Instantiate(pewpew, muzzle.transform.position, muzzle.transform.rotation);
    }
}
