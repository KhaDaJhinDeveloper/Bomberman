using UnityEngine;

public class RatController : EnemyController
{
    #region Input
    [SerializeField] protected KeyPool key;
    protected Rigidbody2D rb;
    protected Animator ani;
    protected EnemyStats stats;
    protected Collider2D colli2D;
    protected SpriteRenderer sprite;
    protected Vector2Int[] direction =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    protected Vector2 moveDirection;
    [SerializeField] protected LayerMask namelayer;
    [SerializeField] protected float distance;
    #endregion

    public Vector2Int[] Direction {  get => direction; } 
    public Rigidbody2D Rb { get => this.rb; }
    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public EnemyStats Stats { get => stats; }
    public Animator Ani { get => ani; }

    protected override void LoadComponents()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.ani = GetComponentInChildren<Animator>();
        this.colli2D = GetComponent<Collider2D>();
        this.stats = GetComponent<EnemyStats>();
        this.sprite = GetComponentInChildren<SpriteRenderer>(); 
    }
    public virtual bool IsWallDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.moveDirection, this.distance, this.namelayer);
        return hit.collider !=null;    
    }
    protected virtual void Reset()
    {
        this.stats.LoadStatsDefault();
        this.colli2D.enabled = true;
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }
    protected virtual void Flip()
    {
        if(this.rb.linearVelocityX >0)
            this.sprite.flipX = false;  
        else this.sprite.flipX = true;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (moveDirection != Vector2.zero)
        {
            Vector3 start = transform.position;
            Vector3 end = start + (Vector3)(moveDirection.normalized * distance);

            Gizmos.DrawLine(start, end);
            Gizmos.DrawSphere(end, 0.1f);
        }
    }
}
