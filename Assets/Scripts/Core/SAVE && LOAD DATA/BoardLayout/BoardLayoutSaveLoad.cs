using UnityEngine;

public class BoardLayoutSaveLoad : Singleton<BoardLayoutSaveLoad>, IData
{
    private const string FILE_DATA_LAYOUTBOARD = "BoardData.json";
    public void SaveData()
    {
        Board board = FindFirstObjectByType<Board>();
        if (board != null )
        {
            BoardLayoutData boardData = new BoardLayoutData(board.mapData);
            JsonFileUtility.SaveToJson(boardData, FILE_DATA_LAYOUTBOARD);
        }
    }
    public void LoadData()
    {
        throw new System.NotImplementedException();
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_LAYOUTBOARD);
    }
}
