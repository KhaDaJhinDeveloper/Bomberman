using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageDropdown : BaseDropDown
{

    protected override void Start()
    {
        base.Start();
        this.dropdown.value = LocalizationManager.Instance.languageIndex;
    }
    protected override void LoadDropDown(int value)
    {
        base.LoadDropDown(value);
        LocalizationManager.Instance.SetLocale(value);
    }
}
