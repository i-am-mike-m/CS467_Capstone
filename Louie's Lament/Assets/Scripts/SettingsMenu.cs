using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    GameState state;
    [SerializeField] TextMeshProUGUI volumePercentText;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] Toggle graderModeToggle;
    [SerializeField] Toggle fullscreenToggle;    

    private void Start()
    {
        state = FindObjectOfType<GameState>();
        InitializeSettings();        
    }

    public void SetVolume (float sliderVolume)
    {
        int intVolume = (int)sliderVolume;
        float decibels = -40 + (sliderVolume / 2.5f); 
        state.SetVolume(sliderVolume);
        audioMixer.SetFloat("volume", decibels);
        volumePercentText.text = intVolume.ToString() + " %";
    }

    public void InitializeSettings()
    {
        // Set up Volume Bar
        float decibels;
        audioMixer.GetFloat("volume", out decibels);
        slider.value = (decibels + 40) * 2.5f;
        int volumeInt = (int)slider.value;
        volumePercentText.text = volumeInt.ToString() + " %";

        // Set up Grader Mode Toggle
        if (state.GetGraderModeEnabled())
        {
            graderModeToggle.isOn = state.GetGraderModeEnabled();
            ToggleGraderMode(); // Toggle back because adjusting .isOn actually FIRES the toggle...
        }

        // Set up Fullscreen Toggle        
        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = Screen.fullScreen;            
        }
        
    }

    public void SetFullscreen (bool fs)
    {
        Screen.fullScreen = fs;
    }

    public void ToggleGraderMode ()
    {
        state.ToggleGraderMode();
    }
}
