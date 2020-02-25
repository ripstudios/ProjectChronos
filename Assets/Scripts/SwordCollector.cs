using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class SwordCollector : MonoBehaviour
{
    public Transform handHold;

    public Rigidbody swordPrefab;

    private Animator anim;

    public Rigidbody sword;

    void Awake() {
        handHold = this.transform.Find("FGC_Male_Char_Adam_Rig/mc_Ad_Hip/mc_Ad_Abdomen/mc_Ad_Chest/mc_Ad_Right Collar/mc_Ad_Right Shoulder/mc_Ad_Right Forearm/mc_Ad_Right Hand");
        // handHold = this.transform.Find("FGC_Male_Char_Adam_Rig");
        Debug.Log("handHold " + handHold);
        if (swordPrefab == null) {
            Debug.LogError("swordPrefab cannot be found");
        }
        anim = GetComponent<Animator>();
    }

    public void CollectSword() {
        sword = Instantiate<Rigidbody>(swordPrefab, handHold);
        sword.transform.localPosition = new Vector3(0, 0, 0);
        sword.isKinematic = true;
    }
}
