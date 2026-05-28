using UnityEngine;

public class EnemyReceived : DamageReceived
{
    private EnemyStats enemyStats;
    protected override void Start()
    {
        base.Start();
        this.enemyStats = GetComponent<EnemyStats>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(Tag.TAG_PLAYER))
        {
            DamageTake damagetake = collision.gameObject.GetComponent<DamageTake>();
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (damagetake != null && stats != null)
                damagetake.TakeDamage(stats, this.enemyStats.damage);
        }
    }
}
