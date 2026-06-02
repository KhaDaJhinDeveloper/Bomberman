using UnityEngine;

public class BoardLayoutSaveLoad : Singleton<BoardLayoutSaveLoad>, IData
{
    private const string FILE_DATA_LAYOUTBOARD = "BoardData.json";
    public void SaveData()
    {
        Board board = FindFirstObjectByType<Board>();
        if (board != null)
        {
            BoardLayoutData boardData = new BoardLayoutData(board.mapData, board.with, board.height);
            JsonFileUtility.SaveToJson(boardData, FILE_DATA_LAYOUTBOARD);
        }
    }
    public void LoadData()
    {
        BoardLayoutData boardData = JsonFileUtility.LoadFromJson<BoardLayoutData>(FILE_DATA_LAYOUTBOARD);
        if (boardData == null) return;
        Board board = FindFirstObjectByType<Board>();
        if (board != null)
        {
            board.with = boardData.with;
            board.height = boardData.height;
            board.mapData = LoadToMap(boardData,board.with, board.height);
        }
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_LAYOUTBOARD);
    }
    public WallType[,] LoadToMap(BoardLayoutData boardData,int with, int height)
    {
        WallType[,]  map = new WallType[with, height];
        for(int i=0; i < with; i++)
            for(int j=0; j < height;j++)
                map[i,j] = (WallType)boardData.wallTypesData[i * height + j];
        return map;
    }
    public bool HasData() => JsonFileUtility.JsonFileExists(FILE_DATA_LAYOUTBOARD);
}
