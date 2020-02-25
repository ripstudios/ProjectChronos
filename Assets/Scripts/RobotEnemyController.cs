using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEnemyController : MonoBehaviour
{
    public Transform rightGunBone;
    public GameObject rifle;

    void Awake()
    {
        GameObject newRifle = (GameObject)Instantiate(rifle);
        newRifle.transform.parent = rightGunBone;
        newRifle.transform.localPosition = Vector3.zero;
        newRifle.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
