using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;
    private LocalizationManager localizationManager;
    private Text text;

    protected virtual void UpdateText()
    {
        InitializeLocManagerAndText();
        text.text = localizationManager.GetLocalizedValue(key);
    }
    
    private void Awake()
    {
        InitializeLocManagerAndText();
        localizationManager.OnLanguageChanged += UpdateText;
    }

    void Start()
    {
        UpdateText();
    }

    private void OnDestroy()
    {
        localizationManager.OnLanguageChanged -= UpdateText;
    }

    private void InitializeLocManagerAndText()
    {
        localizationManager ??= GameObject.FindGameObjectWithTag("LocalizationManager")
            .GetComponent<LocalizationManager>();

        text ??= GetComponent<Text>();
    }
}
