using UnityEngine;
using System.Collections;

public class EnemyShotBehavior : MonoBehaviour {

    public float fastSpeed = 10f;
    public float slowSpeed = 1f;   
	
	// Update is called once per frame
	void Update () {
        if (TimeShift.Instance.fast)
        {
            transform.position += transform.forward * Time.deltaTime * fastSpeed;
        } else
        {
            transform.position += transform.forward * Time.deltaTime * slowSpeed;
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (!c.TryGetComponent(out EnemyShotBehavior beam))
        {
            Destroy(this.gameObject);
        }
    }
}
