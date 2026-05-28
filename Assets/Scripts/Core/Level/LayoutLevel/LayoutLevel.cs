using UnityEngine;
[CreateAssetMenu(menuName = "Bomberman/LayoutLevel")]
public class LayoutLevel : ScriptableObject
{
    [Header("Map")]
    [Range(19, 99)] public int with;
    [Range(11, 99)] public int height;
    [Range(0, 1)] public float wallBreakingDensity;
    public Item[] itemDensity;
    public Enemy[] enemies;
}
[System.Serializable]
public struct Item
{
    public BuffData buff; 
    public int amount;
}
[System.Serializable]
public struct Enemy
{
    public KeyPool keyEnemy;
    //public GameObject enemyPrefab;
    public int amount;
}
