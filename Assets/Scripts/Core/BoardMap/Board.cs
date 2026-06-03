using Assets.Scripts.NameTag;
using System.Collections.Generic;
using UnityEngine;

public enum WallType { Empty, WallFixed, WallBreakable, Spawn, Bomb ,Door}
public class Board : MonoBehaviour
{
    #region Input
    [Header("LayoutLevel")]
    public LayoutLevel levelInput;
    [Header("Map")]
    public int with;
    public int height;
    public WallType[,] mapData ;
    public float wallBreakingDensity;
    public List<Vector2Int> spawnPoint = new List<Vector2Int>();
    public List<Vector2Int> emptysCell;
    public int safeZoneRadius;

    [Header("ListPrefabs")]
    private List<GameObject> wallBreaksList = new ();
    private List<GameObject> wallFixedList = new();
    private List<GameObject> floorList = new ();
    private List<GameObject> buffList = new ();

    [Header("CountPlayer")]
    public int playerCount;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject door;
    #endregion
    void Start()
    {
        LoadLevel();
        SubscribeEvent();
    }
    public void LoadLevel()
    {
        ClearLayoutData();
        if (!BoardLayoutSaveLoad.Instance.HasData())
        {
            GetLayoutLevel();
            GenerateMap();
            RenderMap();
            PlaceBuffs();
            PlaceEnemies();
        }
        else
        {
            GetDataLayout();
            RenderMap();
        }
        EventManager.OP_EventManager.TriggerEvent(EventName.EVENT_CAMERA_SETUP);
    }
    public void LoadNextLevel()
    {
        ClearLayoutData();
        GetLayoutLevel();
        GenerateMap();
        RenderMap();
        PlaceBuffs();
        PlaceEnemies();
    }
    #region Frame
    public void ClearLayoutData()
    {
        this.spawnPoint = new();
        this.emptysCell = new();
        foreach (GameObject go in this.wallBreaksList)
            ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_WALL_BREAKED,go);
        foreach(GameObject go in this.floorList)
            ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_FLOOR, go);
        foreach (GameObject go in this.wallFixedList)
            ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_WALL_FIXED, go);
        foreach (GameObject go in this.buffList)
        {
            BuffPickUp pickup = go.GetComponent<BuffPickUp>();
            if (pickup != null)
                ObjectPooling.Instance.ReturnToPool(pickup.buffData.buffKey, go);
        }
        this.wallFixedList.Clear();
        this.floorList.Clear();
        this.wallBreaksList.Clear();
        this.buffList.Clear();
    }    
    public void GetLayoutLevel()
    {
        this.levelInput = LevelManager.Instance.GetLevel();
        this.with = this.levelInput.with;
        this.height = this.levelInput.height;
        this.wallBreakingDensity = this.levelInput.wallBreakingDensity;
    }    

    public void GetDataLayout()
    {
        BoardLayoutSaveLoad.Instance.LoadData();
    }
    public void GenerateMap()
    {
        if(this.with %2 == 0) this.with++;
        if (this.height % 2 == 0) this.height++;
        this.mapData = new WallType[this.with, this.height];
        PlaceFixedWalls(); //Step1
        SpawnPoint();      //Step3
        PlaceBreakWalls(); //Step2
    }
    public void PlaceFixedWalls()
    {
        this.mapData = new WallType[this.with, this.height];
        for (int x = 0; x < this.with; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                bool isBorder = (x == 0 || y == 0 || x == this.with -1 || y == this.height -1);
                bool isGrid = (x%2 == 0 && y %2 == 0);
                this.mapData[x, y] = (isBorder || isGrid) ? WallType.WallFixed : WallType.Empty;    
            }
        }
    }
    public void PlaceBreakWalls()
    {
        this.emptysCell = new();
        for (int x = 0; x < this.with; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                if(this.mapData[x, y] == WallType.Empty)
                {
                    this.emptysCell.Add(new Vector2Int(x, y));
                }
            }
        }
        for(int i = this.emptysCell.Count -1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = this.emptysCell[i];
            this.emptysCell[i] = this.emptysCell[j];
            this.emptysCell[j] = temp;
        }
        int wallCount = Mathf.RoundToInt(this.emptysCell.Count * this.wallBreakingDensity);
        int placed = 0;
        for(int i = 0; i < this.emptysCell.Count && placed < wallCount ; i++)
        {
            Vector2Int cell = this.emptysCell[i];
            if(!IsNearSpawn(cell, 1))
            {
                this.mapData[cell.x, cell.y] = WallType.WallBreakable;
                placed++;        
            }
        }
        this.emptysCell.RemoveAll(cell => this.mapData[cell.x, cell.y] == WallType.WallBreakable);

    }
    public void PlaceBuffs()
    {
        int inDex = 0;
        for (int i = this.wallBreaksList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = this.wallBreaksList[i];
            this.wallBreaksList[i] = this.wallBreaksList[j];
            this.wallBreaksList[j] = temp;
        }
        foreach (Item item in this.levelInput.itemDensity)
        {
            int amount = 0;
            while (amount < item.amount && inDex < this.wallBreaksList.Count)
            {
                GameObject buffobj = ObjectPooling.Instance.GetPool(item.buff.buffKey);
                if (buffobj == null) continue;

                buffobj.transform.position = this.wallBreaksList[inDex].transform.position;
                this.buffList.Add(buffobj);
                amount++;
                inDex++;
            }        
        }
        this.door.transform.position = this.wallBreaksList[Random.Range(inDex+1, this.wallBreaksList.Count-1)].transform.position;
        this.mapData[(int)this.door.transform.position.x, (int)this.door.transform.position.y] = WallType.Door;
    }
    public void PlaceEnemies()
    {
        int index = 0;

        foreach(var enemy in this.levelInput.enemies)
        {
            int placed = 0;
            while (placed < enemy.amount && index < this.emptysCell.Count)
            {
                Vector2Int cell = this.emptysCell[index];
                index++;
                if (IsNearSpawn(cell, 2)) continue;
                GameObject obj = ObjectPooling.Instance.GetPool(enemy.keyEnemy);
                if (obj == null) continue;

                obj.transform.position = new Vector2(cell.x, cell.y);

                placed++;
                GameStateManager.Instance.IncreaseEnemyDensity();
            }
        }
    }
    public void SpawnPoint()
    {
        Vector2Int[] cornerSpawn = new Vector2Int[]
            {
                new Vector2Int(1, 1), 
                new Vector2Int(1, this.height - 2), 
                new Vector2Int(this.with - 2, 1),
                new Vector2Int(this.with -2, this.height - 2),
            };
        int count = Mathf.Clamp(this.playerCount, 1, 4);
        for(int i = 0; i < count; i++)
        {
            Vector2Int sp = cornerSpawn[i];
            this.spawnPoint.Add (sp);
            this.mapData[sp.x, sp.y] = WallType.Spawn;
            ClearZone(sp, this.safeZoneRadius);
        } 
            
    }
    public void ClearZone(Vector2Int sp, int radius)
    {
        for(int i = -radius ;i <= radius; i++)
        {
            for (int j = - radius ;j <= radius; j++)
            {
                int x = sp.x + i;
                int y = sp.y + j;
                if(!IsBounds(x, y)) continue;
                if (this.mapData[x, y] == WallType.WallFixed) continue;
                if (this.mapData[x, y] != WallType.Spawn) this.mapData[x, y] = WallType.Empty;  
            }    
        } 
            
    }
    public bool IsBounds(int x, int y)
    { 
        return  x >=0 && x < this.with && y >=0 && y < this.height;
    }
    public bool IsNearSpawn(Vector2Int cellPos, int radius)
    {
        foreach (Vector2Int sp in this.spawnPoint)
        {
            if (Mathf.Abs(cellPos.x - sp.x) <= radius && Mathf.Abs(cellPos.y - sp.y) <= radius) return true;
        }
        return false;
    }
    #endregion
    #region Render
    public void RenderMap()
    {
        for(int x = 0; x < this.with; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                GameObject floor = SpawnPrefab(KeyPool.KEY_FLOOR, new Vector2(x, y), this.transform);
                if (floor != null)
                    this.floorList.Add(floor);
                switch (this.mapData[x, y])
                {
                    case WallType.WallFixed:
                        GameObject wallFixed = SpawnPrefab(KeyPool.KEY_WALL_FIXED, new Vector2(x, y), this.transform);
                        if (wallFixed != null)
                            this.wallFixedList.Add(wallFixed);
                        break;
                    case WallType.WallBreakable:
                        GameObject wallbreaked = SpawnPrefab(KeyPool.KEY_WALL_BREAKED, new Vector2(x, y), this.transform);
                        if (wallbreaked != null)
                            this.wallBreaksList.Add(wallbreaked);
                        break;
                    case WallType.Spawn:
                        this.playerPrefab.transform.position = new Vector2(x, y);
                        break;
                    case WallType.Door:
                    {
                        wallbreaked = SpawnPrefab(KeyPool.KEY_WALL_BREAKED, new Vector2(x, y), this.transform);
                        if (wallbreaked != null)
                            this.wallBreaksList.Add(wallbreaked);
                        this.door.transform.position = new Vector2(x, y);
                        break;
                    }                     
                }
            }                 
        }
    }
    #endregion
    #region CheckBombSlot
    public bool BombsExist(Vector2Int pos)  =>  this.mapData[pos.x, pos.y] == WallType.Bomb || this.mapData[pos.x, pos.y] == WallType.WallBreakable;
    public void MarkTheBombLocation(Vector2Int pos) => this.mapData[pos.x, pos.y] = WallType.Bomb;
    public void EmptyLocation(Vector2Int pos)
    {
        if (this.mapData[pos.x, pos.y] == WallType.Door) return;
        this.mapData[pos.x, pos.y] = WallType.Empty;
    } 
    #endregion
    public GameObject SpawnPrefab(KeyPool key, Vector2 pos, Transform parent)
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
    #region Event
    void SubscribeEvent()
    {
        EventManager.OP_EventManager.Subscribe<Vector2Int>(EventName.EVENT_BOARD_EMPTYLOCATION, EmptyLocation);
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_BOARD_LOADLEVEL, LoadNextLevel);
    }    
    private void OnDestroy() //Unsubcribe
    {
        EventManager.OP_EventManager.Unsubscribe<Vector2Int>(EventName.EVENT_BOARD_EMPTYLOCATION, EmptyLocation);
        EventManager.OP_EventManager.Unsubscribe(EventName.EVENT_BOARD_LOADLEVEL, LoadNextLevel);
    }
    #endregion  
}
