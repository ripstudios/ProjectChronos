using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject PanelOpen;
    public GameObject PanelClose;

    public void OpenPanel() {
        if (PanelOpen != null) {
            PanelOpen.SetActive(true);
        }
    }

    public void ClosePanel() {
        if (PanelClose != null) {
            PanelClose.SetActive(false);
        }
    }
}
