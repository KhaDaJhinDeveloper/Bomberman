using UnityEngine;
[System.Serializable]
public class GameStateData
{
    //Player
    public int health;
    public float speed;
    public int bombRange;
    public int extraBomb;
    public bool canPassThroughWall;
    public Vector3 playerPosition;
    //Level
    public int currentLevel;
    public GameStateData() { }
    public GameStateData(int health, float speed, int bombRange, int extraBomb, bool canPassThroughWall, Vector3 position, int currentLevel)
    {
        this.health = health;
        this.speed = speed;
        this.bombRange = bombRange;
        this.extraBomb = extraBomb;
        this.canPassThroughWall = canPassThroughWall;
        this.playerPosition = position;
        this.currentLevel = currentLevel;
    }
}
