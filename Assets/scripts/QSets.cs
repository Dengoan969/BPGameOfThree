using UnityEngine;
using UnityEngine.UI;

public class QSets : MonoBehaviour
{
    public Dropdown dropdown;
    public void CheckDropdown()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
}
