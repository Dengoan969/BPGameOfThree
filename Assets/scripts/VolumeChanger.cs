using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public Slider mySlider2;

    private void Start()
    {
        mySlider2.value = PlayerPrefs.GetFloat("PVolume", 0.7f);
        // PlayerPrefs.DeleteAll();
    }

    public void ChangeVolume(float sliderValue)
    {
        // PlayerPrefs.DeleteKey("CurrentMusic");
        GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic")).GetComponent<AudioSource>().volume = sliderValue;
        
        PlayerPrefs.SetFloat("PVolume", sliderValue);
    }
}
