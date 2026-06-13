using System.Drawing;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    private Collider2D colli;
    private void Start()
    {
        LoadComponents();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tag.TAG_PLAYER)) return;
        PlayerStats stats= collision.gameObject.GetComponent<PlayerStats>();
        if (stats.canPassThroughWall)
        {
            Physics2D.IgnoreCollision(this.colli, collision.collider, true);
        }               
    }
    void LoadComponents()
    {
        this.colli = GetComponent<Collider2D>();
    }
}
