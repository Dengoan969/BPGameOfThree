using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private string currentLanguage;
    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    public static bool isReady;

    public delegate void ChangeLanguageText();

    public event ChangeLanguageText OnLanguageChanged;
        
    public string CurrentLanguage
    {
        get => currentLanguage;
        set
        {
            PlayerPrefs.SetString("Language", value);
            currentLanguage = PlayerPrefs.GetString("Language");
            // LoadLocalizedText(currentLanguage); // ???????
        }
    }

    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }
    
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian
                || Application.systemLanguage == SystemLanguage.Belarusian
                || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }

        currentLanguage = PlayerPrefs.GetString("Language");
    }

    public void LoadLocalizedText(string languageName)
    {
        // var path = $"{Application.streamingAssetsPath}/Languages/{languageName}.json";
        string path = Application.streamingAssetsPath + "/Languages/" + languageName + ".json";
        var dataAsJson = File.ReadAllText(path);
        
            var loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            
            localizedText = 
                loadedData.items.ToDictionary(t => t.key, t => t.value);
            Debug.Log(localizedText["Russian"]);

            CurrentLanguage = languageName;
            
            PlayerPrefs.SetString("Language", languageName);
            currentLanguage = PlayerPrefs.GetString("Language");
            isReady = true;

            OnLanguageChanged?.Invoke();
    }

    public string GetLocalizedValue(string key)
    {
        //TODO Rework with TryGetValue 
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }

        throw new Exception("Not found");
    }

}
