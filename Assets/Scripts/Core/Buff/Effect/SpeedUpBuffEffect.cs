using UnityEngine;

public class SpeedUpBuffEffect : IBuffEffect
{
    public float value;
    public SpeedUpBuffEffect(float value) {  this.value = value; }
    public void Apply(PlayerStats stats)
    {
        stats.speed += stats.speed * value;
    }

    public void Remove(PlayerStats stats)
    {
        stats.speed -= stats.speed * value;
    }
}
