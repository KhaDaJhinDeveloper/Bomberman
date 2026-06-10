using UnityEngine;

public class EnemySaveLoad : Singleton<EnemySaveLoad>, IData
{
    private const string FILE_DATA_ENEMY = "Enemies.json";
    public void SaveData()
    {
        EnemyStats[] enemyStats = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);
        EnemyData enemyData = new EnemyData(enemyStats);
        JsonFileUtility.SaveToJson(enemyData, FILE_DATA_ENEMY);
    }
    public void LoadData()
    {
        EnemyData enemyData = JsonFileUtility.LoadFromJson<EnemyData>(FILE_DATA_ENEMY);
        if (enemyData == null ) return;
        foreach(var enemy in enemyData.enemiesData)
        {
            GameObject obj = ObjectPooling.Instance.GetPool((KeyPool)enemy.Key);
            if(obj == null) continue;
            obj.transform.position = enemy.position;
            GameStateManager.Instance.IncreaseEnemyDensity();
        }    
    }
    public void DeleteData()
    {
        JsonFileUtility.DeleteJsonFile(FILE_DATA_ENEMY);
        EnemyData data = new EnemyData();
    }
    public bool HasData()=> JsonFileUtility.JsonFileExists(FILE_DATA_ENEMY);

}
