using UnityEngine;

public class CreditsSong : MonoBehaviour
{
    public void PlayCredits()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Pause();
        GameObject.Find("Welcome").GetComponent<AudioSource>().Play();
    }

    public void LeaveCredits()
    {
        GameObject.Find("Welcome").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Play();
    }
}
