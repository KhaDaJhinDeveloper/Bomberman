using UnityEngine;

public class BombReceived : DamageReceived
{
    public const int damageBomb = 1;
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.gameObject.CompareTag(Tag.TAG_PLAYER))
        {
            DamageTake damagetake = collision.gameObject.GetComponent<DamageTake>();
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (damagetake != null && stats != null)
                damagetake.TakeDamage(stats, damageBomb);
        }
        else if(collision.gameObject.CompareTag(Tag.TAG_ENEMY))
        {
            DamageTake damagetake = collision.gameObject.GetComponent<DamageTake>();
            EnemyStats stats = collision.gameObject.GetComponent<EnemyStats>();
            if (damagetake != null && stats != null)
                damagetake.TakeDamage(stats, damageBomb);
        }
    }
}
