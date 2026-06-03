using UnityEngine;
[System.Serializable]
public class EnemyData
{
    public DataEnemyDetail[] enemiesData;
    public EnemyData() { }
    public EnemyData(EnemyStats[] data)
    {
        this.enemiesData = new DataEnemyDetail[data.Length];
        for (int i = 0; i < this.enemiesData.Length; i++)
        {
            this.enemiesData[i] = new DataEnemyDetail((int)data[i].Key, data[i].transform.position);
        }
    }
}

[System.Serializable]
public class DataEnemyDetail
{
    public int Key;
    public Vector3 position;
    public DataEnemyDetail() { }
    public DataEnemyDetail(int key, Vector3 position)
    {
        Key = key;
        this.position = position;
    }
}
