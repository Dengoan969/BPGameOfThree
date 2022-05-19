using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Localizator : MonoBehaviour
{
    public string id;
    public void Awake()
    {  
        // если язык выбран...
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            var GameLanguage = PlayerPrefs.GetString("GameLanguage");  //  RU/EN
            ChangeText(GetBetterLocalizedText(id, GameLanguage));
        }
        else
        {  
            // если язык не выбран то английский по умолчанию
            ChangeText(GetBetterLocalizedText(id, "EN"));
        }
    }

    private void ChangeText(string newText)
    {
        // вставляем текст в текстовое поле объекта на котором висит скрипт
        GetComponent<Text>().text = newText;
    }
  
    private string GetBetterLocalizedText(string id, string lang)
    {  
        // вытаскиваем из таблицы значение
        // читаем из Resources/Localization/Localization.csv
        TextAsset mytxtData=(TextAsset)Resources.Load("localization");
        string loc_txt=mytxtData.text;
        string[] rows = loc_txt.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            string[] cuted_row = Regex.Split(rows[i], ";");
            if(id == cuted_row[0])
            {
                if (lang == "EN")
                {
                    return cuted_row[1];
                }
                if(lang == "RU"){
                    return cuted_row[2];
                }
                break;
            }
        }
        return "translation not found";  // если перевод не найден в таблице
    }
}
