using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    string masterVolume = "MasterVolume";
    string musicVolume = "MusicVolume";
    string SFXVolume = "SFXVolume";


     void Start() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume, .25f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolume, .25f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat(SFXVolume, .25f);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
     }


    public void SetMasterVolume() {
        SetVolume(masterVolume, masterVolumeSlider.value);
        PlayerPrefs.SetFloat(masterVolume, masterVolumeSlider.value);
    }

    public void SetMusicVolume() {
        SetVolume(musicVolume, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(musicVolume, musicVolumeSlider.value);
    }

    public void SetSFXVolume() {
        SetVolume(SFXVolume, SFXVolumeSlider.value);
        PlayerPrefs.SetFloat(SFXVolume, SFXVolumeSlider.value);
    }

    void SetVolume(string groupName, float value) {
        float adjustedVolume = Mathf.Log10(value) * 20;
        if (value == 0) {
            adjustedVolume = -80;
        }
        audioMixer.SetFloat(groupName, adjustedVolume);
    }
}
