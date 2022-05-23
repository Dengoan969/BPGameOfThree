using UnityEngine;

public class UniMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayMusic()
    {
        if (audioSource.isPlaying)
            return;

        audioSource.Play();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }
    
    public void StopMusic() => audioSource.Stop();

    public bool IsPlayingRightFuckingNow()
    {
        return audioSource.isPlaying;
    }
    private void CheckByTagAndDestroy(string inpTag)
    {
        var musicObj = GameObject.FindGameObjectsWithTag(inpTag);
        if (musicObj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
