using UnityEngine;

public class ButtonResume : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameStateManager.Instance.Resume();
    }
}
