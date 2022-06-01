using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public Slider superSlider;
    public Slider surroundSlider;
    
    private void Start()
    {
        superSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("PVolume", 0.7f));
        surroundSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SurVolume", 0.6f));
    }

    public void ChangeVolume(float sliderValue)
    {
        GameObject
            .FindWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
            .GetComponent<AudioSource>()
            .volume = sliderValue;
        
        PlayerPrefs.SetFloat("PVolume", sliderValue);
    }

    public void ChangeSurround(float sliderValue)
    {
        var surrounds = new List<string> 
        {
            "EngineIdle", "MainMoneySound", 
            "Fuel", "FixSound", 
            "ObstacleCrash", "DeadCrashAudio", 
            "LampCrash", "FuelRunOut"
            
        }; 
        
        foreach (var sound in surrounds)
        {
            GameObject.FindGameObjectWithTag(sound).GetComponent<AudioSource>().volume = sliderValue;
        }
        
        PlayerPrefs.SetFloat("SurVolume", sliderValue);
    }
}
