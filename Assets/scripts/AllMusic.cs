using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AllMusic : MonoBehaviour
{
    public TMP_Dropdown musicDropdown;
    public static string CurrentTrack;
    public bool RandomPlaying;
    public static AllMusic AllMusicInstance;
    public static readonly List<string> MyTracks = new List<string>
    {
        "LevelOneMusic", "Layla", 
        "DLB_Zajchik", "500miles", 
        "Knight", "MatMehHymn", 
        "TikhiyOgonek", "Malchik_na_9",
        "Ot_Vinta", "GaParadise", 
        "Upgrade", "EmptyDreams",
        "ForGleb", "Syntwave mix",
        "Fortunate Son", "NightDrive", 
        "Bibika", "Lesnik-KiSH"
    };
    
    private void Start()
    {
        musicDropdown
            .SetValueWithoutNotify(MyTracks
                .IndexOf(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic")));
    }

    void Awake()
    {
        AllMusicInstance = this;
        musicDropdown.ClearOptions();
        musicDropdown.AddOptions(MyTracks);
    }

    void Update()
    {
        if (RandomPlaying 
            && !GameObject.FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
                .GetComponent<AudioSource>()
                .isPlaying)
        {
            LoopChanger.LoopChangerInstance.PlayRandomNewVersion();
        }
    }

    private void StopPlayingMusic()
    {
        foreach (var temp in MyTracks
                     .Select(localTag
                         => GameObject
                             .FindGameObjectWithTag(localTag)
                             .GetComponent<AudioSource>())
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
        composition.volume = PlayerPrefs.GetFloat("PVolume", 0.7f);
        PlayerPrefs.SetString("CurrentMusic", MyTracks[newMus]);
        CurrentTrack = MyTracks[newMus];
    }
}
