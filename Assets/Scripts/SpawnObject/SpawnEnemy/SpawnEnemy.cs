using UnityEngine;

public class SpawnEnemy : SpawnBase
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
}
