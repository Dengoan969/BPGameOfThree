using UnityEngine;

public class RestartChecker : MonoBehaviour
{
    void Start()
    {
        var track = PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic");
        if (track == string.Empty || track is null)
        {
            PlayWithConfig("LevelOneMusic");
        }
        else
        {
            PlayWithConfig(track);
        }
    }

    private void PlayWithConfig(string track)
    {
        var audioTrack = GameObject.FindGameObjectWithTag(track).GetComponent<AudioSource>();
        audioTrack.Play();
        audioTrack.volume = PlayerPrefs.GetFloat("PVolume");
    }
}
