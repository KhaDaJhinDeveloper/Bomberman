using UnityEngine;

public class DamageTake : MonoBehaviour
{
    public void TakeDamage(PlayerStats stats, int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
            stats.StartCoroutine(stats.DieEvent());
    }
    public void TakeDamage(EnemyStats stats, int damage)
    {
        stats.heaalth -= damage;
    }
}
