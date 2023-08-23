using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using System.Net;
using System.Linq;

namespace NetworkControl.GamePlayNetwork
{
    public class GPNPlay : NetworkBehaviour
    {
        private int wordCount;

        private int passwordCount;

      

        [Server]
        private void ResetBattleBase()
        {

        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            ResetBattleBase();
        }

        private void OnDestroy()
        {
            UnregisterGPNServerEvent();
        }

        private void RegisterGPNServerEvent()
        {
            GPNServer.instance.OnConnectedPlayerChange += RefreshPlayerUnit;
        }

        private void UnregisterGPNServerEvent()
        {
            GPNServer.instance.OnConnectedPlayerChange -= RefreshPlayerUnit;
        }

        private void RefreshPlayerUnit()
        { 
            
        
        }
    }

    public class GPNPlayWordBackup
    {

    }
}
