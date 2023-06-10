using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PnlRoomState : MonoBehaviour,PnlConnectUnit
{
    public TMP_Text TxtRoomIp;

    public Button BtnDisconnect;
    public Button BtnReady;

    private PnlConnect pnlConnect;

    public void SetOrderRecevier(PnlConnect pnlConnect)
    {
        this.pnlConnect = pnlConnect;
    }

    private void Start()
    {
        BtnAddListener();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void BtnAddListener()
    {
        BtnDisconnect.onClick.AddListener(ClickDisconnect);
        BtnReady.onClick.AddListener(ClickReady);
    }


    private void ClickDisconnect()
    { 
        GameNetworkManager.Instance.Disconnect();
        pnlConnect.ResetConnectPanel();
    }

    private void ClickReady()
    { 
        
    }
}
