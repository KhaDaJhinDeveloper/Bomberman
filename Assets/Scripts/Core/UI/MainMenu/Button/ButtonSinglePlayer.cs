public class ButtonSinglePlayer : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.NewGame());
    }
}
