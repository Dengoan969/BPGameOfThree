using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text score;
    public Text best;

    void Start()
    {
        score = GameObject.Find("score_text").GetComponent<Text>();
        score.text = GameStatistics.Balance.ToString();
        
        best = GameObject.Find("best").GetComponent<Text>();
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
        }
        score.text = GameStatistics.Balance.ToString();
    }
}
