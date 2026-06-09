using Assets.Scripts.NameTag;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playAni;
    public int health;
    public float speed;
    public int bombRange;
    public int extraBomb;
    public bool canPassThroughWall = false;
    private bool isDieath;
    public void Start()
    {
        bool isMultiplayer = Unity.Netcode.NetworkManager.Singleton != null &&
                         Unity.Netcode.NetworkManager.Singleton.IsClient;
        if (isMultiplayer || !GameStateSaveLoad.Instance.HasData() || GameStateSaveLoad.Instance == null)
            StatsDefault();
        this.rb = GetComponent<Rigidbody2D>();
        this.playAni = GetComponent<PlayerAnimation>();
    }
    public void StatsDefault()
    {
        PlayerStatsDefault statsDefault = new PlayerStatsDefault();
        this.health = statsDefault.health;
        this.speed = statsDefault.speed;
        this.bombRange = statsDefault.bombRange;
        this.extraBomb = statsDefault.extraBomb;
        this.canPassThroughWall = statsDefault.canPassThroughWall;
        this.isDieath = false;
    }
    public IEnumerator DieEvent()
    {
        if (!this.isDieath)
        {
            this.rb.bodyType = RigidbodyType2D.Static;
            this.isDieath = true;
            this.playAni.PlayerAnimationDeath();     
            yield return new WaitForSeconds(1f);
            GameStateManager.Instance.GameOver();
            this.rb.bodyType = RigidbodyType2D.Dynamic;
            Time.timeScale = 0f;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
