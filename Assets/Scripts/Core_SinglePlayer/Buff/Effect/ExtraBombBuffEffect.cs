using UnityEngine;

public class ExtraBombBuffEffect : IBuffEffect
{
    public int value;
    public ExtraBombBuffEffect(int value) {  this.value = value; }
    public void Apply(PlayerStats stats)
    {
        stats.extraBomb += value;
    }
    public void Remove(PlayerStats stats)
    {
        stats.extraBomb -= value;
    }
}
