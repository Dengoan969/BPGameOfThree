using UnityEngine;

public class UniMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        CheckByTagAndDestroy("MenuMusic");
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayMusic()
    {
        if (audioSource.isPlaying)
            return;

        audioSource.Play();
    }

    public void StopMusic() => audioSource.Stop();

    private void CheckByTagAndDestroy(string inpTag)
    {
        var musicObj = GameObject.FindGameObjectsWithTag(inpTag);
        
        if (musicObj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
