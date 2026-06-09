using UnityEngine;

public class ButtonSaveData : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameSaveLoadManager.Instance.SaveData();
    }
}
