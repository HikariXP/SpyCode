using Mirror;
using NetworkControl.GamePlayNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.GamePlayOnline
{
    /// <summary>
    /// GamePlayOnline��ʹ�õ�UI�ܹ�������
    /// </summary>
    public class UISystem : MonoBehaviour
    {
        public NetworkManager server;

        private void Start()
        {
            server = GPNServer.singleton;
        }


    }
}

