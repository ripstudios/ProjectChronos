using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            ProtagControlScript.Instance.StageClear();
        }
    }
}
