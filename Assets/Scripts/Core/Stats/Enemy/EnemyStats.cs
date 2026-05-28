using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyStatsData statsData;
    public int heaalth;
    public float speed;
    public int damage;
    public bool canPassThroughWall = false;
    private void Start()
    {
        LoadStatsDefault();
    }
    private void OnDisable()
    {
        LoadStatsDefault();
    }
    public void LoadStatsDefault()
    {
        this.heaalth = this.statsData.heaalth;
        this.speed = this.statsData.speed;
        this.damage = this.statsData.damage;    
        this.canPassThroughWall = this.statsData.canPassThroughWall;
    }
}
