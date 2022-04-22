using UnityEngine;
using UnityEngine.UI;

public class EnduranceBar : MonoBehaviour
{
    private Image enduranceBar;
    void Start()
    {
        enduranceBar = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        enduranceBar.fillAmount = GameStatistics.Endurance;
    }
}
