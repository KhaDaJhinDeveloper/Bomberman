using UnityEngine;

public class SettingSaveLoad: Singleton<SettingSaveLoad>, IData
{
    private const string FILE_DATA_SETTINGS = "SettingsData.json";
    public void SaveData()
    {
        SoundManager soundManager = SoundManager.Instance;
        LocalizationManager localizationManager = LocalizationManager.Instance;
        if(soundManager != null && localizationManager != null)
        {
            SettingsData settingsData = new SettingsData(soundManager.volumeBG, 
                                                       soundManager.volumeSFX,
                                                       localizationManager.languageIndex);
            JsonFileUtility.SaveToJson(settingsData, FILE_DATA_SETTINGS);
        }
    }
    public void LoadData()
    {
        SoundManager soundManager = SoundManager.Instance;
        LocalizationManager localizationManager = LocalizationManager.Instance;
        if ( soundManager != null && localizationManager != null)
        {
            if (!JsonFileUtility.JsonFileExists(FILE_DATA_SETTINGS))
            {
                soundManager.volumeBG = 1;
                soundManager.volumeSFX = 1;
                localizationManager.SetLocale(1);
            }
            else
            {
                SettingsData settingsData = JsonFileUtility.LoadFromJson<SettingsData>(FILE_DATA_SETTINGS);
                soundManager.volumeBG = settingsData.volumeBG;
                soundManager.volumeSFX = settingsData.volumeSFX;
                localizationManager.SetLocale(settingsData.languageIndex);
            }
        }    
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_SETTINGS);
        SettingsData settingsData = new SettingsData();
    }
    public bool HasData() => JsonFileUtility.JsonFileExists(FILE_DATA_SETTINGS);
}
