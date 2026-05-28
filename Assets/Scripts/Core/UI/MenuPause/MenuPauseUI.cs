using Assets.Scripts.NameTag;
using System.Security.Cryptography;
using UnityEngine;

public class MenuPauseUI : MonoBehaviour
{
    public GameObject menuPauseUI;
    void Start()
    {
        SubEvent();
        HideUI();
    }
    public void HideUI()
    {
        this.menuPauseUI.SetActive(false);     
    }   
    public void ShowUI()
    {
        this.menuPauseUI.SetActive(true);
    }
    private void SubEvent()
    {
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_MENUPAUSEUI_SHOW, ShowUI);
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_MENUPAUSEUI_HIDE, HideUI);
    }
    private void OnDestroy()
    {
        if (EventManager.TryGetInstance(out EventManager eventManager))
        {
            eventManager.Unsubscribe(EventName.EVENT_MENUPAUSEUI_SHOW, ShowUI);
            eventManager.Unsubscribe(EventName.EVENT_MENUPAUSEUI_HIDE, HideUI);
        }
    }
}
