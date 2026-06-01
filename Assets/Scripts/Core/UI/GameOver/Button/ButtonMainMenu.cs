using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameSaveLoadManager.Instance.SaveData();
        TransitionScene.Instance.PlayTransition(()=> GameStateManager.Instance.ReturnMainMenu());
    }
}
