using System.Collections.Generic;
using UnityEngine;

public class BoardMapLogic 
{
    public int height;
    public int width;
    public int playerCount;
    public WallType[,] mapData;
    public float wallBreakingDensity;
    public List<Vector2Int> spawnpoints = new();
    public List<Vector2Int> emptyCells = new();
    public void GenerateMap(int width, int height, int playerCount, float wallBreakingDensity, int seed)
    {
        Random.InitState(seed);
        SetLayout(width, height, playerCount, wallBreakingDensity);
        if (this.width % 2 == 0) this.width++;
        if (this.height % 2 == 0) this.height++;
        PlaceFixedWalls();
        SpawnPointPlayer();
        PlaceBeakedWall();
    }

    #region Frame
    public void SetLayout(int width, int height, int playerCount, float wallBreakingDensity)
    {
        this.width = width;
        this.height = height;
        this.playerCount = playerCount;
        this.wallBreakingDensity = wallBreakingDensity;
    }
    public void PlaceFixedWalls()
    { 
        this.mapData = new WallType[this.width, this.height];
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                bool isBorder = (x == 0 || y == 0 || x == this.width - 1 || y == this.height - 1);
                bool isFixedWall = (x%2 == 0 && y%2 == 0);
                this.mapData[x, y] = (isBorder || isFixedWall) ? WallType.WallFixed : WallType.Empty;
            }
        }
    }
    public void SpawnPointPlayer()
    {
        Vector2Int[] cornerSpawn = new Vector2Int[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(1, this.height - 2),
            new Vector2Int(this.width - 2, 1),
            new Vector2Int(this.width -2, this.height - 2)
        };
        int countPlayer = Mathf.Clamp(this.playerCount, 1, 4);
        for (int i = 0; i < cornerSpawn.Length; i++)
        { 
            Vector2Int sp = cornerSpawn[i];
            this.spawnpoints.Add(sp);
            this.mapData[sp.x, sp.y] = WallType.Spawn;
            ClearZone(sp, 2);
        }
    }
    public void PlaceBeakedWall()
    {
        this.emptyCells = new();
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                if (this.mapData[x, y] == WallType.Empty)
                    this.emptyCells.Add(new Vector2Int(x, y));
            }
        }
        for(int i = this.emptyCells.Count -1; i > 0; i--)
        {
            int j = Random.Range(0, i+1);
            var temp = this.emptyCells[i];
            this.emptyCells[i] = this.emptyCells[j];
            this.emptyCells[j] = temp;
        }
        int countWallBearked = Mathf.RoundToInt(this.wallBreakingDensity * this.emptyCells.Count);
        for (int i = 0; i < countWallBearked ; i++)
        {
            Vector2Int cell = this.emptyCells[i];
            if(!IsNearSpawnPoint(cell, 1))
                this.mapData[cell.x, cell.y] = WallType.WallBreakable;
        }
    }
    public bool IsNearSpawnPoint(Vector2Int pos, int radius)
    {
        foreach (var cell in this.spawnpoints)
        {
            if(Mathf.Abs(pos.x - cell.x) <= radius && Mathf.Abs(pos.y - cell.y) <= radius) return true;
        }
        return false; 
    }
    public void ClearZone(Vector2Int pos, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int x = pos.x + i;
                int y = pos.y + j;
                if (!IsBounds(x, y)) continue;
                if (this.mapData[x, y] == WallType.WallFixed) continue;
                if (this.mapData[x, y] != WallType.Spawn) this.mapData[x, y] = WallType.Empty;
            }
        }
    }
    public bool IsBounds(int x, int y)=> x >= 0 && y >= 0 && x < this.width && y < this.height;
    #endregion
    public bool BombsExist(Vector2Int pos) => this.mapData[pos.x, pos.y] == WallType.Bomb || this.mapData[pos.x, pos.y] == WallType.WallBreakable;
    public void MarkTheBombLocation(Vector2Int pos) => this.mapData[pos.x, pos.y] = WallType.Bomb;
    public void EmptyLocation(Vector2Int pos)
    {
        if (this.mapData[pos.x, pos.y] == WallType.Door) return;
        this.mapData[pos.x, pos.y] = WallType.Empty;
    }
}
