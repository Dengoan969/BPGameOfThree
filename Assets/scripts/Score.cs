using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    private Text txt;

    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "Score : " + Game.Balance;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Score : " + Game.Balance;
    }
}
