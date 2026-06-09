using UnityEngine;

public class ButtonRetry : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameSaveLoadManager.Instance.DeleteStateData();
        TransitionScene.Instance.PlayTransition(() =>GameStateManager.Instance.ReTry());
    }
}
