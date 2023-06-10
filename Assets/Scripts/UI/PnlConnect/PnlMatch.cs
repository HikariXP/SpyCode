using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Æ¥Åä½×¶ÎÑ¡ÔñÄ£Ê½µÄUIÂß¼­
/// </summary>
public class PnlMatch : MonoBehaviour, PnlConnectUnit
{
    private PnlConnect pnlConnect;

    public TMP_InputField inputFieldAddress;

    public Button BtnHostMode;
    public Button BtnClientMode;

    private void Start()
    {
        RegisterButtonListener();
    }

    private void RegisterButtonListener()
    {
        BtnHostMode.onClick.AddListener(BtnStartHostClick);
        BtnClientMode.onClick.AddListener(BtnStartClientClick);
    }


    private void BtnStartHostClick()
    {
        StartGameServer(ServerMode.host);
    }

    private void BtnStartClientClick()
    {
        StartGameServer(ServerMode.client);
    }

    /// <summary>
    /// UI¼¤»îGameServer
    /// </summary>
    /// <param name="gameMode"></param>
    public void StartGameServer(ServerMode gameMode)
    {
        if (GameNetworkManager.Instance.mode != Mirror.NetworkManagerMode.Offline)
        {
            Debug.LogError("GameServer already online");
            return;
        }

        switch (gameMode)
        {
            case ServerMode.host:
                GameNetworkManager.Instance.StartHost(); break;
            case ServerMode.client:
                if (!string.IsNullOrWhiteSpace(inputFieldAddress.text))
                {
                    System.Uri uri = new System.Uri("kcp://" + inputFieldAddress.text + ":11451");
                    GameNetworkManager.Instance.StartClient(uri);
                }
                break;
            default:
                Debug.LogWarning("Non Support Mode"); break;
        }

        pnlConnect.ChangePanel(PnlConnect.ConnetPanelOrder.inRoom);
    }

    public void StopGameManager()
    {
        GameNetworkManager.Instance.Disconnect();
    }

    public void SetOrderRecevier(PnlConnect pnlConnect)
    {
        this.pnlConnect = pnlConnect;
    }
}

