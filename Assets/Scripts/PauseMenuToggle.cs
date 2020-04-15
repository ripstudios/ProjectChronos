using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    public new GameObject camera;

    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            Debug.LogError("Canvas Group component not found");
        }
    }

    void Update()
    {
        if (!ProtagControlScript.Instance.dead)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (canvasGroup.interactable)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    camera.GetComponent<CameraFollow>().enabled = true;

                    Time.timeScale = 1f;
                }
                else
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    camera.GetComponent<CameraFollow>().enabled = false;

                    Time.timeScale = 0f;
                }
            }
        }
    }
}
