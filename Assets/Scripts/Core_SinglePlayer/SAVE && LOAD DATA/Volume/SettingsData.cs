using UnityEngine;
[System.Serializable]
public class SettingsData 
{
    public float volumeBG;
    public float volumeSFX;
    public int languageIndex;
    public SettingsData() { }
    public SettingsData
        (float volumeBG, float volumeSFX, int languageIndex)
    {
        this.volumeBG = volumeBG;
        this.volumeSFX = volumeSFX;
        this.languageIndex = languageIndex;
    }
}
