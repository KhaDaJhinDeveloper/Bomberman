using Assets.Scripts.NameTag;
using UnityEngine;

public class ButtonMenupause : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameStateManager.Instance.Pause();
    }
}
