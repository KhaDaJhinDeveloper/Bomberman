using Assets.Scripts.NameTag;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    void Start()
    {
        SubEvent();
        HideUI();
    }
    void Update()
    {
        
    }
    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    { 
        gameObject.SetActive(true); 
    }
    void SubEvent()
    {
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_GAMEOVERUI_SHOW, ShowUI);
        EventManager.OP_EventManager.Subscribe(EventName.EVENT_GAMEOVERUI_HIDE, HideUI);
    }
    private void OnDestroy()
    {
        if (EventManager.TryGetInstance(out EventManager eventManager))
        {
            eventManager.Unsubscribe(EventName.EVENT_GAMEOVERUI_SHOW, ShowUI);
            eventManager.Unsubscribe(EventName.EVENT_GAMEOVERUI_HIDE, HideUI);
        }
    }
}
