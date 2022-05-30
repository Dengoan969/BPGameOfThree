using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [FormerlySerializedAs("PauseMenuUI")] public GameObject pauseMenuUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !GameObject.Find("PanelS").activeSelf)
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
        GameIsPaused = false;
    }

    public void Restart()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        PlayerPrefs.SetString("CurrentMusic", AllMusic.CurrentTrack);
        SceneManager.LoadScene("GameScene");
        ResetStatistics();
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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
        MainCar.speed = 0.3f * MainCar.stageSizes.y;
        PlayerControl.deltaSpeed = 0.01f * MainCar.speed;
        PlayerControl.deltaAngle = 10f;
        GameStatistics.Balance = 0;

    }
}
