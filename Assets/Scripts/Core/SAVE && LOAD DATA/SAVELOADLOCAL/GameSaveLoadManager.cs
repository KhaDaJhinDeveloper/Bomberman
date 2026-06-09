using UnityEngine;

public class GameSaveLoadManager : Singleton<GameSaveLoadManager>, IData
{
    public void SaveData()
    {
        GameStateSaveLoad.Instance.SaveData();
        SettingSaveLoad.Instance.SaveData();
        BoardLayoutSaveLoad.Instance.SaveData();
        BuffSaveLoad.Instance.SaveData();
        EnemySaveLoad.Instance.SaveData();
    }
    public void LoadData()
    {
        GameStateSaveLoad.Instance.LoadData();
        BuffSaveLoad.Instance.LoadData();
        EnemySaveLoad.Instance.LoadData();
    }
    public void DeleteData()
    {
        GameStateSaveLoad.Instance.DeleteData();
        SettingSaveLoad.Instance.DeleteData();
        BoardLayoutSaveLoad.Instance.DeleteData();
        BuffSaveLoad.Instance.DeleteData();
        EnemySaveLoad.Instance.DeleteData();
    }
    public void DeleteStateData()
    {
        GameStateSaveLoad.Instance.DeleteData();
        BoardLayoutSaveLoad.Instance.DeleteData();
        BuffSaveLoad.Instance.DeleteData();
        EnemySaveLoad.Instance.DeleteData();
    }
    public bool HasData()
    {
        return false;
    }
    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        SettingSaveLoad.Instance.SaveData();
    }
}
