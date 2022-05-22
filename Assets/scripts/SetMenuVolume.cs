using UnityEngine;
using UnityEngine.UI;

public class SetMenuVolume : MonoBehaviour
{
    public Slider mySlider;
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().volume =
            PlayerPrefs.GetFloat("MenuVolume", 0.7f);
        mySlider.value = PlayerPrefs.GetFloat("MenuVolume", 0.7f);
    }
}
