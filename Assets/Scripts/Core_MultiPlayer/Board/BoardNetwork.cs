using UnityEngine;
using Unity.Netcode;
public class BoardNetwork : NetworkBehaviour
{
    public int width;
    public int height;
    public int playerCount;
    [Range(0, 1)] public float wallbreakeddestiny;
    private BoardMapLogic boardMapLogic = new();
    private NetworkVariable<int> mapSeed = new();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkPostSpawn();
        if(IsServer)
            StartServerMapSetUp();
    }
    public void StartServerMapSetUp()
    {
        int seed = Random.Range(int.MinValue, int.MaxValue);
        this.mapSeed.Value = seed;
        this.boardMapLogic.GenerateMap(this.width, this.height, this.playerCount,this.wallbreakeddestiny, this.mapSeed.Value);
        ServerRenderMap(this.boardMapLogic.mapData);
    }
    #region RenderMap
    public void ServerRenderMap(WallType[,] mapData)
    {
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                GameObject floor = SpawnPrefab(KeyPool.KEY_FLOOR, new Vector2(x, y), this.transform);
                if (floor != null)
                switch (mapData[x, y])
                {
                    case WallType.WallFixed:
                        GameObject wallFixed = SpawnPrefab(KeyPool.KEY_WALL_FIXED, new Vector2(x, y), this.transform);
                        break;
                    case WallType.WallBreakable:
                        GameObject wallbreaked = SpawnPrefab(KeyPool.KEY_WALL_BREAKED, new Vector2(x, y), this.transform);
                        break;
                    //case WallType.Spawn:
                    //    this.playerPrefab.transform.position = new Vector2(x, y);
                    //    break;
                }
            }
        }
    }
    public GameObject SpawnPrefab(KeyPool key, Vector2 pos, Transform parent = null)
    {
        GameObject obj = ObjectPooling.Instance.GetPool(key, parent);
        if (obj == null)
        {
            Debug.LogError($"Failed to spawn {key} at {pos}. Check pool setup.", this);
            return null;
        }

        obj.transform.position = pos;
        return obj;
    }
    #endregion
    //public void SyncFullMapClientRpc()
    //{

    //}
}
