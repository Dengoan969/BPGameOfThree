using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public void Set_RU()
    {
        // сохраняем пару ключ-значение
        PlayerPrefs.SetString("GameLanguage", "RU");
        // выведем сведетельство того, что игра увидела смену языка
        Debug.Log("Language changed to RUSSIAN");
    }
    public void Set_EN()
    {
        PlayerPrefs.SetString("GameLanguage", "EN");
        Debug.Log("Language changed to ENGLISH");
    }
}