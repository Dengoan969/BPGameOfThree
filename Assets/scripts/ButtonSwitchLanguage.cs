using UnityEngine;

public class BtnSwitchLang: MonoBehaviour
{
    [SerializeField]
    private LocalizationManager localizationManager;

    void OnButtonClick()
    {
        localizationManager.CurrentLanguage = name;
    }
}