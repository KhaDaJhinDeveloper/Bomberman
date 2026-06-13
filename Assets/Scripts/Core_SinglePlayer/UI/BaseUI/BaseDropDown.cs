using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public abstract class BaseDropDown : MonoBehaviour
{
    protected TMP_Dropdown dropdown;
    protected virtual void Start()
    {
        LoadComponents();
        AddOnDropDownValueChange();
    }
    protected virtual void LoadComponents()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }
    protected virtual void AddOnDropDownValueChange()
    {
        this.dropdown.onValueChanged.AddListener(LoadDropDown);
    }
    protected virtual void LoadDropDown(int value)
    {

    }
}
