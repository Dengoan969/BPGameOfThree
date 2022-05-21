using UnityEngine;

public class RestartChecker : MonoBehaviour
{
    void Start()
    {
        var track = PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic");
        if (track == string.Empty || track is null)
        {
            var trackToPlay = GameObject.FindGameObjectWithTag("LevelOneMusic").GetComponent<AudioSource>();
            PlayWithConfig(trackToPlay);
        }
        else
        {
            var trackToPlay = GameObject.FindGameObjectWithTag(track).GetComponent<AudioSource>();
            PlayWithConfig(trackToPlay);
        }
    }

    private void PlayWithConfig(AudioSource track)
    {
        track.Play();
        track.volume = PlayerPrefs.GetFloat("PVolume");
    }
}
