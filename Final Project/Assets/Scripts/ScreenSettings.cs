using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start() {
        resolutions = Screen.resolutions;
        int currentResolutionInt = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length-1);
        Resolution currentResolution = Screen.currentResolution;

        for (int i = 0; i < resolutions.Length; i++) {
            string resolutionString = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
        }
        currentResolutionInt = Math.Min(currentResolutionInt, resolutions.Length -1);
        resolutionDropdown.value = currentResolutionInt;
        SetResolution();
    }

    public void SetResolution() {
        int resIndex = resolutionDropdown.value;
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, true);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
    }
}
