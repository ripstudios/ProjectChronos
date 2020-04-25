using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    // Start is called before the first frame update
    public void Next()
    {
        TimeShift.Instance.stage = 0;
        switch(SceneManager.GetActiveScene().name)
        {
            case "Tutorial":
                SceneManager.LoadScene("Level_1");
                break;
            case "Level_1":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_2":
                SceneManager.LoadScene("Main Menu Fix");
                break;
            default:
                break;
        }
    }
}
