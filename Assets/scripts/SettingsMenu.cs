using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private bool isFullScreen;

    public void AdjustAudioVolume(float sliderValue)
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().volume = sliderValue;
        GameObject.Find("Welcome").GetComponent<AudioSource>().volume = sliderValue;
        PlayerPrefs.SetFloat("MenuVolume", sliderValue);
    }
    
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
