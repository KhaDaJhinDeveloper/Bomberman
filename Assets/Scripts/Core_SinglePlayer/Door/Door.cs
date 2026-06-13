    using Assets.Scripts.NameTag;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isTriggerWall;
    private bool isSpawn;
    private bool isLoadLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.TAG_WALLBREAK))
        {
            this.isTriggerWall = true;
            return;
        }
        else if (collision.gameObject.CompareTag(Tag.TAG_PLAYER))
        {
            if (GameStateManager.Instance.CanNextLevel() && !this.isTriggerWall && !this.isLoadLevel)
            {
                StartCoroutine(NextLevel());
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Explode") && !this.isTriggerWall)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.TAG_WALLBREAK))
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(ExitTriggerWall());
            else
                isTriggerWall = false;
        }
    }
    private IEnumerator SpawnEnemy()
    {
        if(!this.isSpawn)
        {
            this.isSpawn = true;
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < 4; i++)
            {
                GameObject obj = ObjectPooling.Instance.GetPool(KeyPool.KEY_ENEMY_SKELETON);
                GameStateManager.Instance.IncreaseEnemyDensity();
                if (obj == null) continue;
                obj.transform.position = this.transform.position;
            }
            this.isSpawn = false;
        }
    }
    private IEnumerator ExitTriggerWall()
    {
        yield return new WaitForSeconds(0.5f);
        this.isTriggerWall = false;
    }
    private IEnumerator NextLevel()
    {
        if(!this.isLoadLevel)
        {
            this.isLoadLevel = true;
            SoundManager.Instance.StopPlayMusicBG();
            SoundManager.Instance.PlayMusicSFX(SoundManager.Instance.sfx_PassLevel);
            yield return new WaitForSeconds(3f);
            this.isLoadLevel = false;
            TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.NextLevel());
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
