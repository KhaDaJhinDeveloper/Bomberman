using Assets.Scripts.NameTag;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager: Singleton<GameStateManager>
{
    public int enemyDensity = 0;
    #region EnemyDensityControll
    public bool CanNextLevel() => this.enemyDensity == 0;
    public void IncreaseEnemyDensity() => this.enemyDensity++;
    public void ReduceEnemyDensity()
    {
        this.enemyDensity--;
        if (this.enemyDensity < 0) this.enemyDensity = 0;
    }
    #endregion
    #region GameState
    public void NewGame()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.ResetLevel();
        this.enemyDensity = 0;
        SceneManager.LoadScene("SinglePlayer");
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_Play);
    }
    public void ReTry()
    {
        Time.timeScale = 1f;
        this.enemyDensity = 0;
        LevelManager.Instance.ResetLevel();
        EventManager.Instance.TriggerEvent(EventName.EVENT_GAMEOVERUI_HIDE);
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_Play);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_Play);
        EventManager.OP_EventManager.TriggerEvent(EventName.EVENT_BOARD_LOADLEVEL);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        EventManager.Instance.TriggerEvent(EventName.EVENT_MENUPAUSEUI_HIDE);
    }
    public void Pause()
    {        
        EventManager.Instance.TriggerEvent(EventName.EVENT_MENUPAUSEUI_SHOW);
        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        SoundManager.Instance.StopPlayMusicBG();
        SoundManager.Instance.PlayMusicSFX(SoundManager.Instance.sfx_PlayerDie);
        EventManager.Instance.TriggerEvent(EventName.EVENT_GAMEOVERUI_SHOW);
    }
    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayMusicBG(SoundManager.Instance.bg_MainMenu);
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
