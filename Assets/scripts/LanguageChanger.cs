using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public void Set_RU()
    {
        PlayerPrefs.SetString("GameLanguage", "RU");
        Debug.Log("Language changed to Russian");
    }
    
    public void Set_EN()
    {
        PlayerPrefs.SetString("GameLanguage", "EN");
        Debug.Log("Language changed to English");
    }
}