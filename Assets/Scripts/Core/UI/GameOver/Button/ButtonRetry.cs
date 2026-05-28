using UnityEngine;

public class ButtonRetry : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        TransitionScene.Instance.PlayTransition(() =>GameStateManager.Instance.ReTry());
    }
}
