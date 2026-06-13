using UnityEngine;

public class BomRangeBuffEffect : IBuffEffect
{
    public int range;
    public BomRangeBuffEffect(int range) {  this.range = range; }
    public void Apply(PlayerStats stats)
    {
        stats.bombRange += range;
    }
    public void Remove(PlayerStats stats)
    {
        stats.bombRange -= range;
    }
}
