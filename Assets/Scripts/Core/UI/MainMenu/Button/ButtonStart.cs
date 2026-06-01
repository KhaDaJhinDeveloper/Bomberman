using UnityEngine;

public class ButtonStart : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.NewGame());
    }
}
