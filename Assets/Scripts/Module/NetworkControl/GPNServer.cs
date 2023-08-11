using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace NetworkControl.GamePlayNetwork
{
    public class GPNServer : NetworkManager
    {
        private NetworkDiscovery m_Discovery;

        public override void Awake()
        {
            base.Awake();

            Debug.Log("GameServerManager is Awake");

            m_Discovery = gameObject.GetComponent<NetworkDiscovery>();
        }

        public override void OnStartServer()
        {
            //开始前将默认的地址修改为本机地址
            networkAddress = GetDeviceLANAddress();

            base.OnStartServer();

            NetworkServer.RegisterHandler<PlayerAddInfo>(RecieveClientPlayerProfileAndCreate);
            
            //广播
            m_Discovery.AdvertiseServer();
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            NetworkServer.UnregisterHandler<PlayerAddInfo>();

            Debug.Log("Server Stoped");

            m_Discovery.StopDiscovery();
        }

        /// <summary>
        /// [客户端]当客户端链接上服务器
        /// </summary>
        public override void OnClientConnect()
        {
            if (mode == NetworkManagerMode.ClientOnly)
            {
                m_Discovery.StopAllCoroutines();
                m_Discovery.StopDiscovery();

                Debug.Log("[GPNServer]Discovery is Stoped");
            }



            Debug.Log("[GPNServer]As Client Connected");

            //默认是设置了Mirror自己的Ready才允许创建PlayerPref，但是我们用自己的Ready，所以这个得保留
            base.OnClientConnect();

            var playerAddInfo = new PlayerAddInfo()
            {
                playerName = PlayerPrefs.GetString(GPNDefine.PlayerName),
                playerLabel = PlayerPrefs.GetString(GPNDefine.PlayerSignature),
            };

            NetworkClient.Send(playerAddInfo);
        }


        /// <summary>
        /// [服务端]当有新的客户端连接到服务器
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);



            RefreshPlayerUnitState();
        }

        /// <summary>
        /// [服务端]当服务器检测到有玩家离线
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);

            RefreshPlayerUnitState();
        }

        /// <summary>
        /// 刷新玩家信息
        /// </summary>
        public void RefreshPlayerUnitState()
        {
            Debug.Log("Refresh PlayerUnits");
        }



        /// <summary>
        /// 根据用户传递的个人信息创建玩家(这个方法只会在Server层面调用，所以Client收不到的)
        /// </summary>
        public void RecieveClientPlayerProfileAndCreate(NetworkConnectionToClient clientConnection, PlayerAddInfo info)
        {
            GameObject playerTemp = Instantiate(playerPrefab);

            var playerUnit = playerTemp.GetComponent<PlayerUnit>();
            playerUnit.playerName = info.playerName;
            playerUnit.playerSignature = info.playerLabel;

            NetworkServer.AddPlayerForConnection(clientConnection, playerTemp);

            Debug.Log("Server:New Player is Added,with name:" + playerUnit.playerName);

            RefreshPlayerUnitState();
        }

        #region Discovery

        /// <summary>
        /// 房主之外的玩家调用，激活局域网搜索自动加入。
        /// </summary>
        public void StartFindServer()
        {
            m_Discovery.StartDiscovery();
            Debug.Log("Start Discovery Server");
        }

        public void OnDiscoveryFindServer(ServerResponse serverResponse)
        {
            if (isNetworkActive)
            {
                m_Discovery.StopDiscovery();
                return;
            }

            Debug.Log("Discovery Found a Server");



            StartClient(serverResponse.uri);
        }

        #endregion

        #region LowLevel - 底层操作

        public string GetDeviceLANAddress()
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
        /// 主动断开连接
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

        #endregion LowLevel - 底层操作
    }

    /// <summary>
    /// 添加玩家需要传递的信息
    /// </summary>
    public struct PlayerAddInfo : NetworkMessage
    {
        public string playerName;

        public string playerLabel;
    }
}