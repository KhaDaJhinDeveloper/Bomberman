using UnityEngine;

public class PassThroughWallBuffEffect : IBuffEffect
{
    public void Apply(PlayerStats stats)
    {
        stats.canPassThroughWall = true;
    }

    public void Remove(PlayerStats stats)
    {
        stats.canPassThroughWall = false;
    }
}
