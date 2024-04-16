/*
 * Author: CharSui
 * Created On: 2024.04.16
 * Description: 搜索UI
 */
using Module.EventManager;
using NetworkControl.GamePlayNetwork;
using UnityEngine;
using UnityEngine.UI;

public class PnlSearching : MonoBehaviour
{
    public GameObject Go;
    
    public Button BtnStopFindServer;
    
    private void Start()
    {
        RegisterEvent();
    }

    private void OnDestroy()
    {
        UnregisterEvent();
    }

    void RegisterEvent()
    {
        BtnStopFindServer.onClick.AddListener(PlayerDoStopClient);
        EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_START_DISCOVER).Register(Show);
        EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_STOP_DISCOVER).Register(Hide);
    }

    void UnregisterEvent()
    {
        BtnStopFindServer.onClick.RemoveAllListeners();
        EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_START_DISCOVER).Unregister(Show);
        EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_STOP_DISCOVER).Unregister(Hide);
    }

    void Show()
    {
        Go.SetActive(true);
    }

    void Hide()
    {
        Go.SetActive(false);
    }

    void PlayerDoStopClient()
    {
        GPNServer.instance.StopFindServer();
    }
}
