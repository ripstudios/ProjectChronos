using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float rotationSpeed = 0.75f;
    public Transform Target;
    private Transform Player;
    public GameObject characterToFollow;
    float mouseX, mouseY;

    public Transform Obstruction;
    float zoomSpeed = 2f;
    private Vector3 offset;   

    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    public float smoothTime = 1f;
    public float maxSpeed = 50f;

    protected Vector3 currentPositionCorrectionVelocity;
    protected Vector3 currentFacingCorrectionVelocity;

    public Vector3 desiredLoc;
    private float height;
    
    void Start()
    {
        Player = characterToFollow.transform;
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - Player.transform.position;
        distance = offset.magnitude;
        playerPrevPos = Player.transform.position;
        height = transform.position.y;
    }

    private void LateUpdate()
    {
        CamControl();
        // ViewObstructed();
    }
    
    void CamControl()
    {
        if (this.characterToFollow != null)
        {
            Transform camPose = this.characterToFollow.transform.Find("CamPos");
            if (camPose == null)
            {
                Debug.LogError("The character is not assigned CamPos.");
            }
            else
            {
                Vector3 fwd = camPose.TransformDirection(Vector3.forward);
                Vector3 desiredPosition = new Vector3(camPose.position.x, camPose.position.y, camPose.position.z);
                Debug.DrawRay(camPose.position, fwd * 1.5f, Color.green);
                RaycastHit hit;
                if (Physics.Raycast(camPose.position, fwd, out hit, 1.5f))
                {
                    if (hit.collider.gameObject.name != characterToFollow.name && hit.transform.tag != "Door")
                    {
                        desiredPosition = this.characterToFollow.transform.Find("FGC_Male_Char_Adam_Rig/mc_Ad_Hip/mc_Ad_Abdomen/mc_Ad_Chest/mc_Ad_Neck/mc_Ad_Head").position;
                    }
                }
                this.transform.position = Vector3.SmoothDamp(this.transform.position, desiredPosition, ref currentPositionCorrectionVelocity, smoothTime, maxSpeed, Time.deltaTime);
                this.transform.forward = Vector3.SmoothDamp(this.transform.forward, camPose.forward, ref currentFacingCorrectionVelocity, smoothTime, maxSpeed, Time.deltaTime);
            }
        }
        
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        
        // playerMoveDir = Player.transform.position - playerPrevPos;
        
        // if (playerMoveDir != Vector3.zero)
        // {
        //     playerMoveDir = playerMoveDir.normalized;
        //     desiredLoc = Player.transform.position - playerMoveDir * distance;
        //     desiredLoc.y = height;

        //     // transform.position = Vector3.SmoothDamp(transform.position, desiredLoc, ref currentPositionCorrectionVelocity, smoothTime, maxSpeed, Time.deltaTime);
            
        //     playerPrevPos = Player.transform.position;
        // }

        transform.LookAt(Target);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }

        
    }
    

    // void ViewObstructed()
    // {
    //     RaycastHit hit;

    //     if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
    //     {
    //         if (hit.collider.gameObject.tag != "Player")
    //         {
    //             Obstruction = hit.transform;
    //             Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
    //             if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
    //                 transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
    //         }
    //         else
    //         {
    //             Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //             if (Vector3.Distance(transform.position, Target.position) < 4.5f)
    //                 transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
    //         }
    //     }
    // }
}