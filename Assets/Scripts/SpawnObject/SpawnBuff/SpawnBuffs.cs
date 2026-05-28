using UnityEngine;

public class SpawnBuffs : SpawnBase
{
    protected override void Awake()
    {
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
    public void Test()
    {
        CreatePool();
    }
}
