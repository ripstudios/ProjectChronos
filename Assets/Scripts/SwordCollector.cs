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

    public bool hasSword = false;

    void Awake() {
        handHold = this.transform.Find("FGC_Male_Char_Adam_Rig/mc_Ad_Hip/mc_Ad_Abdomen/mc_Ad_Chest/mc_Ad_Right Collar/mc_Ad_Right Shoulder/mc_Ad_Right Forearm/mc_Ad_Right Hand/SwordHoldSpot");
        // handHold = this.transform.Find("FGC_Male_Char_Adam_Rig");
        Debug.Log("handHold " + handHold);
        if (swordPrefab == null) {
            Debug.LogError("swordPrefab cannot be found");
        }
        anim = GetComponent<Animator>();
    }

    void Start() {
        // hasSword = false;
    }

    public void CollectSword() {
        sword = Instantiate<Rigidbody>(swordPrefab, handHold);
        sword.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0)); // set this to modify sword rotation so holding is more realistic
        sword.transform.localPosition = new Vector3(0, 0, 0);
        sword.isKinematic = true;
        hasSword = true;
    }
}
