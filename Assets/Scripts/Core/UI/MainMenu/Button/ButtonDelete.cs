using UnityEngine;

public class ButtonDelete : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        GameSaveLoadManager.Instance.DeleteData();
    }
}
