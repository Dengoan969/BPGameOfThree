using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        StopAllTracksByTag("MenuMusic");
        SceneManager.LoadScene("SampleScene");
    }

    public void ChooseLevel() => SceneManager.LoadScene("LevelChoice");
    
    public void GoToSettingsMenu() => SceneManager.LoadScene("SettingScene");
    
    public void GoToMainMenu() => SceneManager.LoadScene("MenuScene");
    
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
