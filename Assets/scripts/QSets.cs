using UnityEngine;
using UnityEngine.UI;

public class QSets : MonoBehaviour
{
    public Dropdown dropdown;

    public void CheckDropdown()
    {
        /// <summary>
        /// Obsolete code, use TMP_Dropdown handler
        /// </summary>

        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
}
