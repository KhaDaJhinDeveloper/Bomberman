using UnityEngine;

public class ButtonContinue : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.Continue());
    }
}
