using UnityEngine;
using UnityEngine.InputSystem;

public class PlantingBombs : MonoBehaviour
{
    private Board board;
    private PlayerStats playerStats;
    private void Start()
    {
        LoadComponents();
    }
    public void Planting(InputAction.CallbackContext input)    //KeyBoard J
    {

        if (this.playerStats.extraBomb <= 0) return;
        Vector2Int pos = Vector2Int.RoundToInt(this.transform.position);
        if(this.board.BombsExist(pos)) return;
        GameObject bombObj = ObjectPooling.Instance.GetPool(KeyPool.KEY_BOMB);       
        if (bombObj == null) return;
        SoundManager.Instance.PlayMusicSFX(SoundManager.Instance.sfx_PlantingBomb);
        bombObj.transform.position = new Vector3(pos.x, pos.y, 0);
        this.board.MarkTheBombLocation(pos);
        this.playerStats.extraBomb--;
    }
    void LoadComponents()
    {
        this.board = GameObject.FindFirstObjectByType<Board>();
        this.playerStats = GetComponent<PlayerStats>();
    }
}
