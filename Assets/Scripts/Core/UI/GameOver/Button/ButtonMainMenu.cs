using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();

        TransitionScene.Instance.PlayTransition(()=> GameStateManager.Instance.ReturnMainMenu());
    }
}
