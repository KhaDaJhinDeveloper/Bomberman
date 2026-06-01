using UnityEngine;

public class ButtonSinglePlayer : BaseButton
{
    [SerializeField] private GameObject continueWarning;
    protected override void OnClick()
    {
        base.OnClick();
        if(GameStateSaveLoad.Instance.HasData())
            this.continueWarning.SetActive(true);
        else
            TransitionScene.Instance.PlayTransition(() => GameStateManager.Instance.NewGame());
    }
}
