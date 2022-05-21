using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AllMusic : MonoBehaviour
{
    public TMP_Dropdown musicDropdown;
    public static string CurrentTrack;
    private static readonly List<string> MyTracks = new List<string>
    {
        "LevelOneMusic", "Layla", 
        "DLB_Zajchik", "500miles", 
        "Knight", "MatMehHymn", 
        "TikhiyOgonek", "Malchik_na_9",
        "Ot_Vinta", "GaParadise", 
        "Upgrade", "EmptyDreams"
    };

    
    void Awake()
    {
        musicDropdown.ClearOptions();
        musicDropdown.AddOptions(MyTracks);
    }

    private void StopPlayingMusic()
    {
        foreach (var temp in MyTracks
                     .Select(localTag
                         => GameObject
                             .FindGameObjectWithTag(localTag).GetComponent<AudioSource>())
                     .Where(temp => temp.isPlaying))
        {
            temp.Stop();
        }
    }
    
    public void ChangeMusic(int newMus)
    {
        var composition = GameObject.FindGameObjectWithTag(MyTracks[newMus]).GetComponent<AudioSource>();
        StopPlayingMusic();
        composition.Play();
        composition.volume = PlayerPrefs.GetFloat("PVolume");
        PlayerPrefs.SetString("CurrentMusic", MyTracks[newMus]);
        CurrentTrack = MyTracks[newMus];
    }
}
