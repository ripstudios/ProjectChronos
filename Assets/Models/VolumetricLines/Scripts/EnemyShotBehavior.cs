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
        if (c.TryGetComponent(out Rigidbody rb))
        {
            ProtagControlScript player = rb.gameObject.GetComponent<ProtagControlScript>();
            if (player != null)
            {
                ProtagControlScript.Instance.GameOver();
            }
        }
        Destroy(this.gameObject);
    }
}
