using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public int stageNumber = 0;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (stageNumber > TimeShift.Instance.stage)
            {
                TimeShift.Instance.stage = stageNumber;
            }
        }
    }
}
