using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class Localizator : MonoBehaviour
{
    public string id;
    
    public void Update()
    {
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            var gameLanguage = PlayerPrefs.GetString("GameLanguage");
            ChangeText(GetBetterLocalizedText(id, gameLanguage));
        }
        else
        {
            ChangeText(GetBetterLocalizedText(id, "EN"));
        }
    }

    private void ChangeText(string newText) => GetComponent<TMP_Text>().text = newText;
    
  
    private static string GetBetterLocalizedText(string id, string lang)
    {
        var myTxtData = (TextAsset)Resources.Load("localization");
        var rows = myTxtData.text.Split('\n');
        
        for (var i = 1; i < rows.Length; i++)
        {
            var splitRow = Regex.Split(rows[i], ";");
            
            if (id == splitRow[0])
            {
                switch (lang)
                {
                    case "EN":
                        return splitRow[1];
                    case "RU":
                        return splitRow[2];
                }

                break;
            }
        }
        return "Translation Not Found";
    }
}
