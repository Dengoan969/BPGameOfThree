using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text score;
    public Text best;
    public GameObject gameover;
    
    void Start()
    {
        score.text = GameStatistics.Balance.ToString();
        best.text = "Best: " + PlayerPrefs.GetInt("best_res", 0);
    }

    void Update()
    {
        if (GameStatistics.IsGameOver)
        {
            if (GameStatistics.Balance > PlayerPrefs.GetInt("best_res", 0))
            {
                PlayerPrefs.SetInt("best_res", GameStatistics.Balance);
            }

            gameover.SetActive(true);
        }
        score.text = GameStatistics.Balance.ToString();
    }
}
