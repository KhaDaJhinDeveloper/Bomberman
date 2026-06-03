using UnityEngine;

public class VolumeSaveLoad : Singleton<VolumeSaveLoad>, IData
{
    private const string FILE_DATA_VOLUME = "VolumeData.json";
    public void SaveData()
    {
        SoundManager soundManager = SoundManager.Instance;
        if(soundManager != null)
        {
            VolumeData volumeData = new VolumeData( SoundManager.Instance.volumeBG, 
                                                    SoundManager.Instance.volumeSFX );
            JsonFileUtility.SaveToJson(volumeData, FILE_DATA_VOLUME);
        }
    }
    public void LoadData()
    {
        SoundManager soundManager = SoundManager.Instance;
        if( soundManager != null)
        {
            if (!JsonFileUtility.JsonFileExists(FILE_DATA_VOLUME))
            {
                soundManager.volumeBG = 1;
                soundManager.volumeSFX = 1;
            }
            else
            {
                VolumeData volumeData = JsonFileUtility.LoadFromJson<VolumeData>(FILE_DATA_VOLUME);
                soundManager.volumeBG = volumeData.volumeBG;
                soundManager.volumeSFX = volumeData.volumeSFX;
            }
        }    
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_VOLUME);
        VolumeData volumeData = new VolumeData();
    }
    public bool HasData() => JsonFileUtility.JsonFileExists(FILE_DATA_VOLUME);
}
