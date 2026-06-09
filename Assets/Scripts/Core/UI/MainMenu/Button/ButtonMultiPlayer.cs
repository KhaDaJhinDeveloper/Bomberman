using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ButtonMultiPlayer : BaseButton
{
    [SerializeField] private GameObject warningNoInternet;
    protected override void OnClick()
    {
        base.OnClick();        
        if (!IsConnectInternet())
            SceneManager.LoadScene("MultiPlayer");
        else
            this.warningNoInternet.SetActive(true);
    }
    private bool IsConnectInternet() => Application.internetReachability == NetworkReachability.NotReachable;
}
