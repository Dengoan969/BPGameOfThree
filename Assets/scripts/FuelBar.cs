using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private Image fuelBar;
    void Start()
    {
        fuelBar = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        fuelBar.fillAmount = GameStatistics.Fuel;
    }
}
