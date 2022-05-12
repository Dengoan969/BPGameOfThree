using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    private bool isFullScreen;
    public AudioMixer sAudioMixer;
    // Resolution[] rsl;
    // List<string> resolutions;
    public Dropdown dropdown;
    
    public void AdjustAudioVolume(float sliderValue) 
        => sAudioMixer.SetFloat("masterVolume", sliderValue);
    
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    // public void Awake()
    // {
    //     resolutions = new List<string>();
    //     rsl = Screen.resolutions;
    //     
    //     foreach (var res in rsl)
    //     {
    //         resolutions.Add(res.width + "x" + res.height);
    //     }
    //     
    //     dropdown.ClearOptions();
    //     dropdown.AddOptions(resolutions);
    // }
    //
    //
    // public void ChangeResolution(int newRes) 
    //     => Screen.SetResolution(rsl[newRes].width, rsl[newRes].height, isFullScreen);
    
}
