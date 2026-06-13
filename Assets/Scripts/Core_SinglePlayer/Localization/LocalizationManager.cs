using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager>
{
    //private Locale currentLocale => LocalizationSettings.SelectedLocale;
    public int languageIndex = 1;
    private async void Start()
    {
        await LocalizationSettings.InitializationOperation.Task;
    }
    public void SetLocale(int inDex)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        if(inDex >= 0 && inDex < locales.Count)
        {
            LocalizationSettings.SelectedLocale = locales[inDex];
            this.languageIndex = inDex;
        }
    }
}
