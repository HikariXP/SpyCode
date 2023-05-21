using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

/// <summary>
/// 局域网战局总体管理，负责整体的网络响应
/// </summary>
public class GameNetworkManager : NetworkManager
{
    //public static GameNetworkManager Instance { get; private set; }

    //public override void Awake()
    //{
    //    base.Awake();
    //    if (Instance != null)
    //    {
    //        Debug.LogError("instance is existed");
    //        return;
    //    }

    //    Instance = this;
    //}
    public Dictionary<NetworkConnectionToClient, PlayerUnit> playerList = new Dictionary<NetworkConnectionToClient, PlayerUnit>();

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

    //这是客户端调用的，客户端发送信息
    public override void OnClientConnect()
    {
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
    }

    /// <summary>
    /// 当有新的客户端连接到服务器
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
    }

    /// <summary>
    /// 当服务器检测到有玩家离线
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        playerList.Remove(conn);
    }



    /// <summary>
    /// 根据用户传递的个人信息创建玩家
    /// </summary>
    public void OnCreatePlayerInLANRoom(NetworkConnectionToClient clientConnection,PlayerAddInfo info)
    {
        GameObject playerTemp = Instantiate(playerPrefab);

        var playerUnit = playerTemp.GetComponent<PlayerUnit>();
        playerUnit.playerName = info.playerName;
        playerUnit.playerLabel = info.playerLabel;

        playerList.Add(clientConnection, playerUnit);

        NetworkServer.AddPlayerForConnection(clientConnection, playerTemp);
        Debug.Log("Server:New Player is Added,with name:"+ playerUnit.playerName);
        
    }

    /// <summary>
    /// 获取本机IP地址
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
}

/// <summary>
/// 添加玩家需要传递的信息
/// </summary>
public struct PlayerAddInfo : NetworkMessage
{
    public string playerName;

    public string playerLabel;
}

/// <summary>
/// 本机模式
/// </summary>
public enum GameMode
{
    host,
    client
}