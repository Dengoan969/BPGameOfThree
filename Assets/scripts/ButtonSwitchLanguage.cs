using UnityEngine;

public class ButtonSwitchLanguage : MonoBehaviour
{
    [SerializeField]
    private LocalizationManager localizationManager;
 
    public void OnButtonClick()
    {
        localizationManager.CurrentLanguage = name;
    }
}
