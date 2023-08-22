using Mirror;
using NetworkControl.GamePlayNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.GamePlayOnline
{
    /// <summary>
    /// GamePlayOnline下使用的UI总管理器。
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

