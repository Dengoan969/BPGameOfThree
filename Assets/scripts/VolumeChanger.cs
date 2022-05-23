using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public Slider superSlider;
    
    private void Start()
    {
        
        superSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("PVolume", 0.7f));
        // PlayerPrefs.DeleteAll();
    }

    public void ChangeVolume(float sliderValue)
    {
        // PlayerPrefs.DeleteKey("CurrentMusic");
        GameObject.FindWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic")).GetComponent<AudioSource>().volume = sliderValue;
        
        PlayerPrefs.SetFloat("PVolume", sliderValue);
    }
}
