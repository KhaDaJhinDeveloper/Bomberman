using UnityEngine;

public class SpawnBombs : SpawnBase
{
    protected override void Start()
    {
        base.Start();
        CreatePool();
    }
}
