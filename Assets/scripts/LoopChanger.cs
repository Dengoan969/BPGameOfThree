using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class LoopChanger : MonoBehaviour
{
    private bool isLoopPressed;
    private bool isRandomPressed;
    private AudioSource nextTrackToPlay;
    
    public Button loopButton;
    public Button shuffleButton;
    
    private Sprite initialLoopSprite;
    private Sprite initialShuffleSprite;
    
    public Sprite newLoopSprite;
    [FormerlySerializedAs("shuffleSprite")] public Sprite newShuffleSprite;
    
    public static LoopChanger LoopChangerInstance;
    
    public void LoopCurrentSong()
    {
        isLoopPressed = !isLoopPressed;
        
        PlayerPrefs.SetString("Looping", isLoopPressed ? "on" : "off");
        ChangeLoopImage(isLoopPressed, loopButton);
        
        GameObject
            .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
            .GetComponent<AudioSource>()
            .loop = isLoopPressed;
        
        Debug.Log($"Loop turned to {isLoopPressed}");
    }
    
    public void Start()
    {
        initialLoopSprite = loopButton.image.sprite;
        initialShuffleSprite = shuffleButton.image.sprite;
        
        isLoopPressed = PlayerPrefs.GetString("Looping", "off") != "on";
        LoopCurrentSong();
    }
    
    private void Awake()
    {
        LoopChangerInstance = this;
    }

    public void PlayTracksByRandom()
    {
        isRandomPressed = !isRandomPressed;
        
        ChangeLoopImage(isRandomPressed, shuffleButton);
        
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

    private void ChangeLoopImage(bool isPressed, Selectable button)
    {
        if (button == loopButton)
            button.image.sprite = isPressed ? newLoopSprite : initialLoopSprite;
        else
            button.image.sprite = isPressed ? newShuffleSprite : initialShuffleSprite;
    }
}
