using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.GamePlayOnline.PnlBattle
{
    public class PnlBattle : MonoBehaviour
    {
        private PlayerUnit playerUnit;
    
        public PnlWord pnlWord;

        public void Init(PlayerUnit localPlayerUnit)
        {
            playerUnit = localPlayerUnit;
            // pnlWord.RefreshWordDisplay(playerUnit.team);
        }

        /// <summary>
        /// 获得密钥作为Sender的玩家不能解码
        /// </summary>
        public void OnGetCode()
        {
        
        }

        public void ConfirmCode()
        {
        
        }
    
    

    }
}


