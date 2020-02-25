using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSword : MonoBehaviour
{
    void OnTriggerEnter(Collider c) {
        Debug.Log("collision");
        if (c.attachedRigidbody != null) {
            SwordCollector sc = c.attachedRigidbody.gameObject.GetComponent<SwordCollector>();
            if (sc != null) {
                EventManager.TriggerEvent<CollectSwordEvent, Vector3>(c.transform.position);
                Destroy(this.gameObject);
                sc.CollectSword();
            }
        }
    }
}
