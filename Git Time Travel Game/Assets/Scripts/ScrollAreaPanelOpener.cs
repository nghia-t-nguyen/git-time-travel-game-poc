using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAreaPanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public static bool setActivation;

    private void Awake() {
        setActivation = false;
    }

    public void TogglePanel() {
        if (Panel != null) {
            setActivation = !setActivation;
            Panel.SetActive(setActivation);
        }
    }
}
