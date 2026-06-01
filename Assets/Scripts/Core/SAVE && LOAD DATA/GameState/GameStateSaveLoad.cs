using UnityEngine;

public class GameStateSaveLoad : Singleton<GameStateSaveLoad>, IData
{
    private const string FILE_DATA_GAMESTATE = "GameSate.json";
    public void SaveData()
    {
        PlayerStats playerStats = GameObject.FindWithTag(Tag.TAG_PLAYER).GetComponent<PlayerStats>();
        LevelManager levelManager = LevelManager.Instance;
        if (levelManager != null && playerStats != null)
        {
            GameStateData gameStateData = new GameStateData(    playerStats.health,
                                                                playerStats.speed, 
                                                                playerStats.bombRange, 
                                                                playerStats.extraBomb, 
                                                                playerStats.canPassThroughWall, 
                                                                playerStats.transform.position,
                                                                levelManager.currentLevel   );
            JsonFileUtility.SaveToJson(gameStateData, FILE_DATA_GAMESTATE);                
        }
    }
    public void LoadData()
    {
        GameStateData gameStateData = JsonFileUtility.LoadFromJson<GameStateData>(FILE_DATA_GAMESTATE);
        PlayerStats playerStats = GameObject.FindWithTag(Tag.TAG_PLAYER).GetComponent<PlayerStats>();
        LevelManager levelManager = LevelManager.Instance;
        if (levelManager != null && playerStats != null)
        {
            playerStats.health = gameStateData.health;
            playerStats.speed = gameStateData.speed;    
            playerStats.bombRange = gameStateData.bombRange;
            playerStats.extraBomb = gameStateData.extraBomb;
            playerStats.canPassThroughWall = gameStateData.canPassThroughWall;
            playerStats.transform.position = gameStateData.playerPosition;
            levelManager.currentLevel = gameStateData.currentLevel; 
        }
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_GAMESTATE);
        GameStateData gameStateData = new();
    }
    public bool HasData() => JsonFileUtility.JsonFileExists(FILE_DATA_GAMESTATE);
}
