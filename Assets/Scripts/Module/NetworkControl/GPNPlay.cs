using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using System.Net;

namespace NetworkControl.GamePlayNetwork
{
    public class GPNPlay : NetworkBehaviour
    {
        private int wordCount;

        private int passwordCount;

        //对于多队伍的同步需要再看看文档。

        private void InitBattleBase()
        { 
            
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            InitBattleBase();

            
        }
    }

    public class GPNPlayWordBackup
    {

    }
}
