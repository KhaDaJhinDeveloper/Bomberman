using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    private Button button;
    protected virtual void Start()
    {
        LoadComponents();
        AddOnClickEvent();
    }
    protected virtual void LoadComponents()
    {
        this.button = GetComponent<Button>();
    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(OnClick);
    }
    protected virtual void OnClick()
    {
        SoundManager.Instance.PlayMusicSFX(SoundManager.Instance.sfx_PressButton);
    }
}
