using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTextLoader : MonoBehaviour
{
    public void LoadIntroText() {
        SceneManager.LoadScene("Main Menu - Intro Text");
    }
}
