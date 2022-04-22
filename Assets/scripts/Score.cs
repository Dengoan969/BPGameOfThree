using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    private Text score;

    void Start()
    {
        score = gameObject.GetComponent<Text>();
        score.text = GameStatistics.Balance.ToString();
    }

    void Update()
    {
        score.text = GameStatistics.Balance.ToString();
    }
}
