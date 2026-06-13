using System.Collections;
using UnityEngine;

public class Rat_Behavior : RatController
{
    private float time;
    private float maxTime;
    private bool isResting;
    private bool isDeath;
    protected override void Start()
    {
        base.Start();
        this.maxTime = Random.Range(1, 4);
        ChangeState(new Rat_MoveState(this));
    }
    protected override void Update()
    {
        if (this.isDeath) return;
        base.Update();
        Flip();
        this.time += Time.deltaTime;
        if(!this.isResting && (IsWallDirection() || this.time > this.maxTime))
        {
            StartCoroutine(Rest());
        }    
        if(this.stats.heaalth <=0 && !this.isDeath)
        {
            this.isDeath = true;
            this.ani.SetTrigger("die");
            StartCoroutine(Death());
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    IEnumerator Rest()
    {
        this.isResting = true;
        ResetPosition();    
        ChangeState(new Rat_IdleState(this));
        yield return new WaitForSeconds(Random.Range(0.2f, 1.5f));
        ChangeState(new Rat_MoveState(this));
        this.time = 0;
        this.maxTime = Random.Range(1, 4);
        this.isResting = false;
    }
    IEnumerator Death()
    {
        SoundManager.Instance.PlayMusicSFX(SoundManager.Instance.sfx_EnemyDeath);
        this.currentState = null;
        this.colli2D.enabled = false;
        this.rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Reset();
        this.isDeath = false;
        ObjectPooling.Instance.ReturnToPool(this.stats.Key, this.gameObject);
    }    
    void ResetPosition()
    {
        this.transform.position = new Vector2(Mathf.RoundToInt(this.transform.position.x), 
                                              Mathf.RoundToInt(this.transform.position.y));
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.isResting = false; 
        if (GameStateManager.TryGetInstance(out GameStateManager gameStateManager))
            gameStateManager.ReduceEnemyDensity();
    }
}
