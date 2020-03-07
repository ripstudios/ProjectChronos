using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
        
    void OnTriggerEnter(Collider c)
    {
        if (ProtagControlScript.Instance.attacking)
        {
            Debug.Log("Hit!");
            Destroy(c.gameObject);
        } else
        {
            // Do what when hit whilst not attacking?
        }
    }
}
