using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllMusic : MonoBehaviour
{
    public TMP_Dropdown musicDropdown;
    private static readonly List<string> myTracks = new List<string> 
        {"LevelOneMusic", "Layla", "DLB_Zajchik", "500miles"};

    public static string currentTrack;
    void Awake()
    {
        musicDropdown.ClearOptions();
        musicDropdown.AddOptions(myTracks);
    }

    private void StopPlayingMusic()
    {
        foreach (var localTag in myTracks)
        {
            var temp = GameObject.FindGameObjectWithTag(localTag).GetComponent<AudioSource>();
            if (temp.isPlaying)
            {
                temp.Stop();
            }
        }
    }
    
    public void ChangeMusic(int newMus)
    {
        var composition = GameObject.FindGameObjectWithTag(myTracks[newMus]).GetComponent<AudioSource>();
        // GameObject.FindGameObjectWithTag("LevelOneMusic").GetComponent<AudioSource>().playOnAwake = false;
        StopPlayingMusic();
        composition.Play();
        PlayerPrefs.SetString("CurrentMusic", myTracks[newMus]);
        currentTrack = myTracks[newMus];
    }
}
