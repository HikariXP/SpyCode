using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ս������������������������Ӧ
/// </summary>
public class GameNetworkManager : NetworkManager
{
    public static GameNetworkManager Instance { get; private set; }

    /// <summary>
    /// [�¼�]������������仯:���������
    /// </summary>
    public event Action<int> OnPlayerAmountChange;

    public override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("instance is existed");
            return;
        }

        Instance = this;
    }

    public Dictionary<NetworkConnectionToClient, PlayerUnit> playerList = new Dictionary<NetworkConnectionToClient, PlayerUnit>();

    public override void OnStartHost()
    {
        base.OnStartHost();
    }

    public override void OnStartServer()
    {
        networkAddress = GetDeviceLANAddress();

        base.OnStartServer();
        NetworkServer.RegisterHandler<PlayerAddInfo>(OnCreatePlayerInLANRoom);

        Debug.Log("Server Started:" + networkAddress);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkServer.UnregisterHandler<PlayerAddInfo>();

        Debug.Log("Server Stoped");
    }

    //���ǿͻ��˵��õģ��ͻ��˷�����Ϣ
    public override void OnClientConnect()
    {
        //Ĭ����������Mirror�Լ���Ready��������PlayerPref�������������Լ���Ready����������ñ���
        base.OnClientConnect();

        var playerAddInfo = new PlayerAddInfo()
        {
            playerName = "1",
            playerLabel = "label",
        };

        NetworkClient.Send(playerAddInfo);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        //֪ͨ
        OnPlayerAmountChange?.Invoke(playerList.Count);
    }

    /// <summary>
    /// �����µĿͻ������ӵ�������
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);


    }

    /// <summary>
    /// ����������⵽���������
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        playerList.Remove(conn);

        //֪ͨ
        OnPlayerAmountChange?.Invoke(playerList.Count);
    }





    /// <summary>
    /// �����û����ݵĸ�����Ϣ�������(�������ֻ����Server������ã�����Client�ղ�����)
    /// </summary>
    public void OnCreatePlayerInLANRoom(NetworkConnectionToClient clientConnection,PlayerAddInfo info)
    {
        GameObject playerTemp = Instantiate(playerPrefab);

        var playerUnit = playerTemp.GetComponent<PlayerUnit>();
        playerUnit.playerName = info.playerName;
        playerUnit.playerSignature = info.playerLabel;

        playerList.Add(clientConnection, playerUnit);

        NetworkServer.AddPlayerForConnection(clientConnection, playerTemp);


        Debug.Log("Server:New Player is Added,with name:"+ playerUnit.playerName);
    }

    /// <summary>
    /// ��ȡ����IP��ַ
    /// </summary>
    /// <returns></returns>
    private string GetDeviceLANAddress()
    {
        string AddressIP = string.Empty;
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
            {
                AddressIP = _IPAddress.ToString();
            }
        }
        return AddressIP;
    }

    /// <summary>
    /// �����Ͽ�����
    /// </summary>
    public void Disconnect()
    {
        switch (mode)
        {
            case NetworkManagerMode.Host:
                StopHost();
                break;
            case NetworkManagerMode.ClientOnly:
                StopClient();
                break;
        }
    }
}

/// <summary>
/// ��������Ҫ���ݵ���Ϣ
/// </summary>
public struct PlayerAddInfo : NetworkMessage
{
    public string playerName;

    public string playerLabel;
}

/// <summary>
/// ����ģʽ
/// </summary>
public enum ServerMode
{
    host,
    client,
    offline
}