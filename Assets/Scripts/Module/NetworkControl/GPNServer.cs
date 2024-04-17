using Mirror;
using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Module.EventManager;
using UnityEngine;


namespace NetworkControl.GamePlayNetwork
{
    public class GPNServer : NetworkManager
    {
        /// <summary>
        /// ?????GameOnlineScene??Object????
        /// </summary>
        public static GPNServer instance;

        private NetworkDiscovery m_Discovery;

        public override void Awake()
        {
            base.Awake();

            Debug.Log("GameServerManager is Awake");

            m_Discovery = gameObject.GetComponent<NetworkDiscovery>();

            instance = this;
        }

        public override void OnStartServer()
        {
            //???????????????????????
            networkAddress = GetDeviceLANAddress();

            base.OnStartServer();

            NetworkServer.RegisterHandler<PlayerAddInfo>(RecieveClientPlayerProfileAndCreate);
            
            //??
            m_Discovery.AdvertiseServer();

            Debug.Log("[GPNServer]Start Server, AdvertiseServer");
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            NetworkServer.UnregisterHandler<PlayerAddInfo>();

            Debug.Log("Server Stoped");

            m_Discovery.StopDiscovery();
        }

        /// <summary>
        /// [?????]??????????????????
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

            //???????????Mirror?????Ready????????PlayerPref?????????????????Ready??????????????
            base.OnClientConnect();

            var playerAddInfo = new PlayerAddInfo()
            {
                playerName = PlayerPrefs.GetString(GPNDefine.PlayerName),
                playerLabel = PlayerPrefs.GetString(GPNDefine.PlayerSignature),
            };

            NetworkClient.Send(playerAddInfo);
        }


        /// <summary>
        /// ?????????????????????????(????????????Server????????????Client???????)
        /// </summary>
        public void RecieveClientPlayerProfileAndCreate(NetworkConnectionToClient clientConnection, PlayerAddInfo info)
        {
            GameObject playerTemp = Instantiate(playerPrefab);

            var playerUnit = playerTemp.GetComponent<PlayerUnit>();
            playerUnit.playerName = info.playerName;
            playerUnit.playerSignature = info.playerLabel;

            NetworkServer.AddPlayerForConnection(clientConnection, playerTemp);

            Debug.Log("Server:New Player is Added,with name:" + playerUnit.playerName);
        }

        #region Discovery

        /// <summary>
        /// 开始寻找局域网内的服务器
        /// </summary>
        public void StartFindServer()
        {
            m_Discovery.StartDiscovery();
            EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_START_DISCOVER).Notify();
            Debug.Log($"[{nameof(GPNServer)}]Start Discovery Server");
        }

        public void StopFindServer()
        {
            m_Discovery.StopDiscovery();
            EventManager.instance.TryGetNoArgEvent(EventDefine.SERVER_STOP_DISCOVER).Notify();
            Debug.Log($"[{nameof(GPNServer)}]Stop Discovery Server");
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

        #region LowLevel - ??????

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
        /// ???????????
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

        #endregion LowLevel - ??????
    }

    /// <summary>
    /// ?????????????????
    /// </summary>
    public struct PlayerAddInfo : NetworkMessage
    {
        public string playerName;

        public string playerLabel;
    }
}