using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI volumePercentText;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider slider;    
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle graderModeToggle;

    GameState state = null;
    bool active = false;

    private void Start()
    {
        state = FindObjectOfType<GameState>();
        InitializeSettings();                
    }

    private void InitializeSettings()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!active)
            {
                Pause();
            }
            else if (active)
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        active = true; 
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        active = false;
    }
    
    public void SetVolume(float sliderVolume)
    {
        int intVolume = (int)sliderVolume;
        float decibels = -40 + (sliderVolume / 2.5f);
        state.SetVolume(sliderVolume);
        audioMixer.SetFloat("volume", decibels);
        volumePercentText.text = intVolume.ToString() + " %";
    }

    public void SetFullscreen(bool fs)
    {
        Screen.fullScreen = fs;
    }

    public void ToggleGraderMode()
    {
        state.ToggleGraderMode();        
    }
}
