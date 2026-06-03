using UnityEngine;

public class ButtonStart : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameSaveLoadManager.Instance.DeleteStateData();
        TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.NewGame());
    }
}
