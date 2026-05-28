using Assets.Scripts.NameTag;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider2D colli;
    private Collider2D playerCollider;
    private Board board;
    private PlayerStats playerStats;
    public float timeDisableCollision;
    public float timeExplosion;
    private Vector2Int[] direction =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    };
    private void Awake()
    {
        LoadComponents();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableCollider(this.timeDisableCollision));
        StartCoroutine(TriggerExplosion(this.timeExplosion));
    }
    IEnumerator DisableCollider(float duration)
    {
        Physics2D.IgnoreCollision(this.colli, this.playerCollider, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreCollision(this.colli, this.playerCollider, false);
    }    
    IEnumerator TriggerExplosion(float duration)
    {
        yield return new WaitForSeconds(duration);
        Explode(GetPos(), this.playerStats.bombRange);
        this.playerStats.extraBomb++;
        ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_BOMB, this.gameObject);
        EventManager.OP_EventManager.TriggerEvent(EventName.EVENT_BOARD_EMPTYLOCATION, GetPos());
    }    
    Vector2Int GetPos()
    {
        Vector2 pos = transform.position;
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }   
    void Explode(Vector2Int original, int radius)
    {
        foreach(var pos in this.direction)
        {
            ExplodeCell(original);
            for (int i = 1; i <= radius; i++)
            { 
                Vector2Int cellPos = original + pos * i;
                WallType type = this.board.mapData[cellPos.x, cellPos.y];
                if ((type == WallType.WallFixed))
                    break;
                else if(type == WallType.WallBreakable)
                {
                    ExplodeCell(cellPos);
                    this.board.EmptyLocation(cellPos);
                    break;
                }    
                else if(type == WallType.Empty || type == WallType.Spawn)
                {
                    ExplodeCell(cellPos);
                } 
            }
        }
    }

    void ExplodeCell(Vector2Int pos)
    {
        GameObject explodeEffect = ObjectPooling.Instance.GetPool(KeyPool.KEY_EXPLODE);
        if (explodeEffect == null) return;

        explodeEffect.transform.position = new Vector3(pos.x, pos.y, 0);
    }
    void LoadComponents()
    {
        this.colli = GetComponent<Collider2D>();
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.playerStats = GameObject.FindWithTag(Tag.TAG_PLAYER).GetComponent<PlayerStats>();
        this.playerCollider = GameObject.FindWithTag(Tag.TAG_PLAYER).GetComponent<Collider2D>();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
