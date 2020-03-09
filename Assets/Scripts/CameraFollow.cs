using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float rotationSpeed = 5.0f;
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
        offset = transform.position - Player.transform.position;
        distance = offset.magnitude;
        playerPrevPos = Player.transform.position;
        height = transform.position.y;
    }

    private void Update()
    {
        CamControl();
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

        transform.LookAt(Target);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

            // TODO: Also allow rotation with keys
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }

        
    }
    
}