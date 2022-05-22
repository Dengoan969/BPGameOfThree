using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    public void ChangeVolume(float sliderValue)
    {
        GameObject
                .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
                .GetComponent<AudioSource>()
                .volume = sliderValue;
        
        PlayerPrefs.SetFloat("PVolume", sliderValue);
    }
}
