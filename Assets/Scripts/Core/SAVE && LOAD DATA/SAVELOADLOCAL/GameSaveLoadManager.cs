using UnityEngine;

public class GameSaveLoadManager : Singleton<GameSaveLoadManager>, IData
{
    public void SaveData()
    {
        GameStateSaveLoad.Instance.SaveData();
        VolumeSaveLoad.Instance.SaveData();
        BoardLayoutSaveLoad.Instance.SaveData();
    }
    public void LoadData()
    {
        GameStateSaveLoad.Instance.LoadData();
    }
    public void DeleteData()
    {
        GameStateSaveLoad.Instance.DeleteData();
        VolumeSaveLoad.Instance.DeleteData();
        BoardLayoutSaveLoad.Instance.DeleteData();
    }

}
