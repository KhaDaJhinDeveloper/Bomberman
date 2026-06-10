using UnityEngine;
using UnityEngine.InputSystem;

public class BuffSaveLoad : Singleton<BuffSaveLoad>,IData
{
    private const string FILE_DATA_BUFF = "Buff.json";
    public void SaveData()
    {
        BuffPickUp[] buffArray = FindObjectsByType<BuffPickUp>(FindObjectsSortMode.None);
        BuffITemData buffData = new BuffITemData(buffArray);
        JsonFileUtility.SaveToJson(buffData, FILE_DATA_BUFF);
    }
    public void LoadData()
    {
        BuffITemData buffData = JsonFileUtility.LoadFromJson<BuffITemData>(FILE_DATA_BUFF);
        Board board = FindFirstObjectByType<Board>();
        if (buffData == null) return;
        foreach(var buff in buffData.buffData)
        {
            GameObject obj = ObjectPooling.Instance.GetPool((KeyPool)buff.BuffKey);
            if (obj == null) continue;
            obj.transform.position = buff.position;
            board.Bufflist.Add(obj);
        }    
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_BUFF);
        BuffITemData buffData = new();
    }
    public bool HasData()=> JsonFileUtility.JsonFileExists(FILE_DATA_BUFF);
}
