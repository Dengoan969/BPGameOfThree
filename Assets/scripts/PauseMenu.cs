using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    [FormerlySerializedAs("PauseMenuUI")] public GameObject pauseMenuUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && !GameObject.Find("PanelS").activeSelf)
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
        gameIsPaused = false;
        Time.timeScale = 1f;
        PlayerPrefs.SetString("CurrentMusic", AllMusic.CurrentTrack);
        SceneManager.LoadScene("GameScene");
        ResetStatistics();
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        GameStatistics.Reset();
    }

    private static void ResetStatistics()
    {
        GameStatistics.Fuel = 1f;
        GameStatistics.Endurance = 1f;
        GameStatistics.IsGameOver = false;
    }
}
