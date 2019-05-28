using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameState state;
    [SerializeField] TextMeshProUGUI volumePercentText;
    [SerializeField] AudioMixer audioMixer;

    public void SetVolume (float sliderVolume)
    {
        int intVolume = (int)sliderVolume;
        float decibels = -40 + (sliderVolume / 2.5f); 
        state.SetVolume(sliderVolume);
        audioMixer.SetFloat("volume", decibels);
        volumePercentText.text = intVolume.ToString() + " %";
    }

    public void SetFullscreen (bool fs)
    {
        Screen.fullScreen = fs;
    }

    public void ToggleGraderMode ()
    {
        state.toggleGraderMode();
    }
}
