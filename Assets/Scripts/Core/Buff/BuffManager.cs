using System;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    private PlayerStats playerStats;
    public static BuffManager S_BuffInstance {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        S_BuffInstance = this;       
    }
    public void ApplyBuff(BuffData data)
    {
        if (data == null) return;
        if(this.playerStats == null)
            this.playerStats = GameObject.FindWithTag(Tag.TAG_PLAYER).GetComponent<PlayerStats>();
        IBuffEffect effect = CreateEffect(data);
        effect.Apply(this.playerStats);
    }

    IBuffEffect CreateEffect(BuffData data)
    {
        switch(data.type)
        {
            case BuffType.SpeedUp:
                SpeedUpBuffEffect speedBuff = new SpeedUpBuffEffect(data.value);
                return speedBuff;               
            case BuffType.BomRange:
                BomRangeBuffEffect bombRange = new BomRangeBuffEffect(Mathf.RoundToInt(data.value));
                return bombRange;
            case BuffType.ExtraBomb:
                ExtraBombBuffEffect extraBomb = new ExtraBombBuffEffect(Mathf.RoundToInt(data.value));
                return extraBomb;
            case BuffType.PassThroughWall:
                PassThroughWallBuffEffect passWall = new PassThroughWallBuffEffect();
                return passWall;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}
