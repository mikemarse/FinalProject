using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIMenu : MonoBehaviour
{

    [SerializeField] bool closedByDefault = true;
    [SerializeField] Canvas settings;

    void Awake()
    {
        if (closedByDefault) {
            CloseMenu();
        }
        else {
            OpenMenu();
        }
    }

    public void OpenMenu() {
        Debug.Log("Open the canvas");
        GetComponent<Canvas>().enabled = true;
    }

    public void CloseMenu() {
        GetComponent<Canvas>().enabled = false;
    }
}
