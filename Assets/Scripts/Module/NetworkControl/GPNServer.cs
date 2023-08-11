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
            //��ʼǰ��Ĭ�ϵĵ�ַ�޸�Ϊ������ַ
            networkAddress = GetDeviceLANAddress();

            base.OnStartServer();

            NetworkServer.RegisterHandler<PlayerAddInfo>(RecieveClientPlayerProfileAndCreate);
            
            //�㲥
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
        /// [�ͻ���]���ͻ��������Ϸ�����
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

            //Ĭ����������Mirror�Լ���Ready��������PlayerPref�������������Լ���Ready����������ñ���
            base.OnClientConnect();

            var playerAddInfo = new PlayerAddInfo()
            {
                playerName = PlayerPrefs.GetString(GPNDefine.PlayerName),
                playerLabel = PlayerPrefs.GetString(GPNDefine.PlayerSignature),
            };

            NetworkClient.Send(playerAddInfo);
        }


        /// <summary>
        /// [�����]�����µĿͻ������ӵ�������
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);



            RefreshPlayerUnitState();
        }

        /// <summary>
        /// [�����]����������⵽���������
        /// </summary>
        /// <param name="conn"></param>
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);

            RefreshPlayerUnitState();
        }

        /// <summary>
        /// ˢ�������Ϣ
        /// </summary>
        public void RefreshPlayerUnitState()
        {
            Debug.Log("Refresh PlayerUnits");
        }



        /// <summary>
        /// �����û����ݵĸ�����Ϣ�������(�������ֻ����Server������ã�����Client�ղ�����)
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
        /// ����֮�����ҵ��ã���������������Զ����롣
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

        #region LowLevel - �ײ����

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

        #endregion LowLevel - �ײ����
    }

    /// <summary>
    /// ��������Ҫ���ݵ���Ϣ
    /// </summary>
    public struct PlayerAddInfo : NetworkMessage
    {
        public string playerName;

        public string playerLabel;
    }
}