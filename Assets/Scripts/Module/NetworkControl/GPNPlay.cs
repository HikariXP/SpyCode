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

        //���ڶ�����ͬ����Ҫ�ٿ����ĵ���

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
