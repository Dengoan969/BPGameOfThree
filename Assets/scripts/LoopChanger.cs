using UnityEngine;
using Random = System.Random;

public class LoopChanger : MonoBehaviour
{
    private bool isLoopPressed;
    private bool isRandomPressed;
    private AudioSource nextTrackToPlay;

    public static LoopChanger LoopChangerInstance;
    public void LoopCurrentSong()
    {
        isLoopPressed = !isLoopPressed;
        GameObject
            .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
            .GetComponent<AudioSource>()
            .loop = isLoopPressed;
        Debug.Log($"Loop turned to {isLoopPressed}");
    }

    private void Awake()
    {
        LoopChangerInstance = this;
    }

    public void PlayTracksByRandom()
    {
        isRandomPressed = !isRandomPressed;
        
        if(isRandomPressed)
        {
            PlayRandomNewVersion();
        }
        else
        {
            AllMusic.AllMusicInstance.RandomPlaying = false;
        }
        
        Debug.Log($"Playing Random turned to {isRandomPressed}");
    }

    public void PlayRandomNewVersion()
    {
        var newRandomSeed = new Random();
        var nextTrackName = AllMusic.MyTracks[newRandomSeed.Next(0, AllMusic.MyTracks.Count)];
        var nextTrack = GameObject.FindGameObjectWithTag(nextTrackName).GetComponent<AudioSource>();
        nextTrackToPlay = nextTrack;
        GameObject
            .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
            .GetComponent<AudioSource>()
            .Stop();
            
        nextTrackToPlay.Play();
            
        AllMusic.AllMusicInstance.musicDropdown.value = AllMusic.MyTracks.IndexOf(nextTrackName);
        AllMusic.AllMusicInstance.RandomPlaying = true;
        nextTrackToPlay.loop = false;
        PlayerPrefs.SetString("CurrentMusic", nextTrackName);
        nextTrackToPlay.volume = PlayerPrefs.GetFloat("PVolume", 0.7f);
    }
}
