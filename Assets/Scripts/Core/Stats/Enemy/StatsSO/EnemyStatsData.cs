using UnityEngine;
[CreateAssetMenu(menuName = "Bomberman/EnemyStats")]
public class EnemyStatsData: ScriptableObject
{
    public int heaalth;
    public float speed;
    public int damage;
    public bool canPassThroughWall = false;
}
