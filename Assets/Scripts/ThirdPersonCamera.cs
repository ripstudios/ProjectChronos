using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float smoothTime = 1f;        // a public variable to adjust smoothing of camera motion
    public float maxSpeed = 50f;         // max speed camera can move
    public GameObject characterToFollow; // the character to follow
    
    private
    
    protected Vector3 currentPositionCorrectionVelocity;
    protected Vector3 currentFacingCorrectionVelocity;

    void LateUpdate()
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
    }
}
