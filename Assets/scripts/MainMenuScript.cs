using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame() =>SceneManager.LoadScene("GameScene");
    public void QuitGame() => Application.Quit();
}