using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsLoader : MonoBehaviour
{
    public void LoadInstructions() {
        Debug.Log("clicked instructions");
        SceneManager.LoadScene("Main Menu - Instructions");
    }
}
