using UnityEngine;

public class SpawnWall : SpawnBase
{
    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }
    protected override void Start()
    {
        base.Start();

    }
    protected override void CreatePool()
    {
        base.CreatePool();
    }
}
