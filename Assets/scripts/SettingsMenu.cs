using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    private bool isFullScreen;
    public AudioMixer sAudioMixer;

    
    public void AdjustAudioVolume(float sliderValue) 
        => sAudioMixer.SetFloat("masterVolume", sliderValue);
    
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
