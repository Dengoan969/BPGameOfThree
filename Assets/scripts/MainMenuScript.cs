using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        StopAllTracksByTag("MenuMusic");
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<UniMusic>().PlayMusic();
    }

    public void QuitGame() => Application.Quit();

    private void StopAllTracksByTag(string inpTag)
    {
        var allTracksPlaying = CollectByTag(inpTag);
        foreach (var track in allTracksPlaying)
        {
            track.GetComponent<UniMusic>().StopMusic();
        }
    }

    private IEnumerable<GameObject> CollectByTag(string inpTag) 
        => GameObject.FindGameObjectsWithTag(inpTag);
}