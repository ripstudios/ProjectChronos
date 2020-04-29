using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed;
    public float fastSpeed = 1.0f;
    public float slowSpeed = 0.1f;

    public GameObject[] waypoints;

    private int currWaypoint;
    private Vector3 velocity;
    private float speedMultiplier;

    // Start is called before the first frame update
    private void Start()
    {
        this.currWaypoint = -1;
        this.SetNextWaypoint();
        this.speedMultiplier = this.fastSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (TimeShift.Instance.fast)
        {
            this.speedMultiplier = this.fastSpeed;
        }
        else
        {
            this.speedMultiplier = this.slowSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, this.waypoints[this.currWaypoint].transform.position) < Vector3.Distance(this.transform.position, this.transform.position + velocity * this.speedMultiplier * Time.deltaTime))
        {
            this.transform.position = this.waypoints[this.currWaypoint].transform.position;
            this.SetNextWaypoint();
        }
        else
        {
            this.transform.position += velocity * this.speedMultiplier * Time.deltaTime;
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
            this.velocity = (this.waypoints[this.currWaypoint].transform.position - this.transform.position).normalized * speed;
        }
    }
}
