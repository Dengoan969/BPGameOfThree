using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    private static bool gameIsPaused;
    [FormerlySerializedAs("PauseMenuUI")] public GameObject pauseMenuUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
        ResetStatistics();
    }

    private void Pause()
    {
        /*foreach (var tmp in 
                 AllMusic
                     .myTracks
                     .Where(tmp 
                         => GameObject
                             .FindGameObjectWithTag(tmp)
                             .GetComponent<AudioSource>()
                             .isPlaying))
        {
            pausedThis = tmp;
        }*/

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        GameStatistics.Reset();
        //ResetStatistics();
    }

    private static void ResetStatistics()
    {
        GameStatistics.Fuel = 1f;
        GameStatistics.Endurance = 1f;
        GameStatistics.Balance = 0;
        GameStatistics.IsGameOver = false;
    }
}
