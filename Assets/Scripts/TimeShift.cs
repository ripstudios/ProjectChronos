using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShift : MonoBehaviour
{
    public static TimeShift Instance { get; private set; }

    public bool fast = true;
    public Slider hud;
    public float maxMana = 10f;
    public int stage = 0;
    public bool respawned = false;

    private float mana;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        mana = maxMana;
        hud.maxValue = maxMana;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            fast = !fast;
        }

        if (!fast)
        {
            mana -= Time.deltaTime;
        } else
        {
            mana = Mathf.Min(mana += Time.deltaTime, maxMana);
        }

        if (mana <= 0 && !fast)
        {
            Debug.Log("Out of time!");
            fast = true;
        }
        hud.value = mana;
        
        if (stage >= 2)
        {
            respawned = true;
        }
    }
}
