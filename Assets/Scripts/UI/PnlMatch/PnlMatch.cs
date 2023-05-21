using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PnlMatch : MonoBehaviour
{
    public List<GameObject> PnlList = new List<GameObject>();

    public TMP_InputField inputFieldAddress;

    //ui进度，0:模式选择、1:房间等待
    private int m_UILevel = 0;

    public void SetUILevel(int index)
    {
        if (index <= 0) index = 0;
        if(index > 3) index = 3;
        m_UILevel = index;


        SetUI();
    }

    private void SetUI()
    {
        foreach (GameObject go in PnlList)
        { 
            go.SetActive(false);
        }

        PnlList[m_UILevel].SetActive(true);
    }

    /// <summary>
    /// UI激活GameServer
    /// </summary>
    /// <param name="gameMode"></param>
    public void StartGameManager(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.host:
                GameNetworkManager.singleton.StartHost(); break;
            case GameMode.client:
                if (!string.IsNullOrWhiteSpace(inputFieldAddress.text))
                {
                    System.Uri uri = new System.Uri("kcp://" + inputFieldAddress.text + ":11451");
                    GameNetworkManager.singleton.StartClient(uri);
                }
                break;
            default:
                Debug.LogWarning("Non Support Mode"); break;
        }
    }
}

