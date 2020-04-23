using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class HumanEnemyController : MonoBehaviour
{
    private enum AIState
    {
        Patrol,
        Attack
    };

    public int weapon = 3;
    public GameObject pewpew;
    public GameObject[] waypoints;
    public float fastSpeed;
    public float slowSpeed;
    public float rotationSmoothSpeed;
    public GameObject guardAreaMinimumX;
    public GameObject guardAreaMinimumZ;
    public GameObject guardAreaMaximumX;
    public GameObject guardAreaMaximumZ;

    private AIState aiState;
    private Animator anim;
    private int currWaypoint;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private GameObject weaponObject;
    private float speed;
    private float speedMultiplier;

    private string objectName;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("weapon", weapon);
        this.navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        objectName = this.gameObject.name;

        DeactiveHairAndFace();
        int hair = Random.Range(0, 7);
        SelectHair(hair);
        int face = Random.Range(1, 12);
        SelectFace(face);

        ChooseWeapon(weapon);

        this.currWaypoint = -1;
        this.speed = this.navMeshAgent.speed;
        this.speedMultiplier = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
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
                    this.navMeshAgent.isStopped = false;
                    this.navMeshAgent.stoppingDistance = this.weapon == 3 ? 1 : 8;
                    this.navMeshAgent.speed = this.weapon == 3 ? 12 : 4;
                    this.navMeshAgent.acceleration = this.weapon == 3 ? 12 : 5;
                    this.speedMultiplier = 1.75f;
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
            audioManager.Play("Human Death");
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

    void DeactiveHairAndFace()
    {
        DeactivateSkinnedMesh(objectName + "/Hair_FlatTop");
        DeactivateSkinnedMesh(objectName + "/Hair_Messy");
        DeactivateSkinnedMesh(objectName + "/Hair_Modern");
        DeactivateSkinnedMesh(objectName + "/Hair_Short");
        DeactivateSkinnedMesh(objectName + "/Hair_SidePart");
        DeactivateSkinnedMesh(objectName + "/Hair_SlickedBack");
        DeactivateSkinnedMesh(objectName + "/Hair_Thick");

        DeactivateSkinnedMesh(objectName + "/m01");
        DeactivateSkinnedMesh(objectName + "/m02");
        DeactivateSkinnedMesh(objectName + "/m03");
        DeactivateSkinnedMesh(objectName + "/m04");
        DeactivateSkinnedMesh(objectName + "/m05");
        DeactivateSkinnedMesh(objectName + "/m06");
        DeactivateSkinnedMesh(objectName + "/m07");
        DeactivateSkinnedMesh(objectName + "/m08");
        DeactivateSkinnedMesh(objectName + "/m09");
        DeactivateSkinnedMesh(objectName + "/m10");
        DeactivateSkinnedMesh(objectName + "/m11");
    }

    void DeactivateMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    void DeactivateSkinnedMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        renderer.enabled = false;
    }

    void ActivateMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.enabled = true;
    }

    void ActivateSkinnedMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        renderer.enabled = true;
    }

    void SelectHair(int i)
    {
        switch(i)
        {
            case 0:
                ActivateSkinnedMesh(objectName + "/Hair_FlatTop");
                break;

            case 1:
                ActivateSkinnedMesh(objectName + "/Hair_Messy");
                break;

            case 2:
                ActivateSkinnedMesh(objectName + "/Hair_Modern");
                break;

            case 3:
                ActivateSkinnedMesh(objectName + "/Hair_Short");
                break;

            case 4:
                ActivateSkinnedMesh(objectName + "/Hair_SidePart");
                break;

            case 5:
                ActivateSkinnedMesh(objectName + "/Hair_SlickedBack");
                break;

            case 6:
                ActivateSkinnedMesh(objectName + "/Hair_Thick");
                break;
        }
    }

    void SelectFace(int i)
    {
        switch (i)
        {
            case 1:
                ActivateSkinnedMesh(objectName + "/m01");
                break;

            case 2:
                ActivateSkinnedMesh(objectName + "/m02");
                break;

            case 3:
                ActivateSkinnedMesh(objectName + "/m03");
                break;

            case 4:
                ActivateSkinnedMesh(objectName + "/m04");
                break;

            case 5:
                ActivateSkinnedMesh(objectName + "/m05");
                break;

            case 6:
                ActivateSkinnedMesh(objectName + "/m06");
                break;

            case 7:
                ActivateSkinnedMesh(objectName + "/m07");
                break;

            case 8:
                ActivateSkinnedMesh(objectName + "/m08");
                break;

            case 9:
                ActivateSkinnedMesh(objectName + "/m09");
                break;

            case 10:
                ActivateSkinnedMesh(objectName + "/m10");
                break;

            case 11:
                ActivateSkinnedMesh(objectName + "/m11");
                break;
        }
    }

    void ActivateWeapon(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        obj.SetActive(true);
    }

    void DeactivateWeapon(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        obj.SetActive(false);
    }

    void ChooseWeapon(int i)
    {
        string rifle = objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Rifle";
        string spacePistol = objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpacePistol";
        string spaceRifleOld = objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpaceRifleOld";
        string broadsword = objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Broadsword";
        switch (i)
        {
            case 0:
                ActivateWeapon(rifle);
                DeactivateWeapon(spacePistol);
                DeactivateWeapon(spaceRifleOld);
                DeactivateWeapon(broadsword);
                this.weaponObject = GameObject.Find(rifle);
                break;

            case 1:
                DeactivateWeapon(rifle);
                ActivateWeapon(spacePistol);
                DeactivateWeapon(spaceRifleOld);
                DeactivateWeapon(broadsword);
                this.weaponObject = GameObject.Find(spacePistol);
                break;

            case 2:
                DeactivateWeapon(rifle);
                DeactivateWeapon(spacePistol);
                ActivateWeapon(spaceRifleOld);
                DeactivateWeapon(broadsword);
                this.weaponObject = GameObject.Find(spaceRifleOld);
                break;

            case 3:
                DeactivateWeapon(rifle);
                DeactivateWeapon(spacePistol);
                DeactivateWeapon(spaceRifleOld);
                ActivateWeapon(broadsword);
                this.weaponObject = GameObject.Find(broadsword);
                break;

            default:
                break;
        }
    }

    private void Fire()
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

    public void CreateBeam()
    {
        Transform muzzleTransform = weaponObject.transform.Find("Muzzle");
        AudioSource blasterSource = weaponObject.transform.Find("PewPew").gameObject.GetComponent<AudioSource>();
        blasterSource.PlayOneShot(blasterSource.clip);
        Instantiate(pewpew, muzzleTransform.position, muzzleTransform.rotation);
    }

    public void ActivateSword()
    {
        weaponObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DectivateSword()
    {
        weaponObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}
