using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.GamePlayOnline
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

        public void RefreshWordDisplay(List<int> wordIndexs)
        {
            pnlWord.RefreshWordDisplay(wordIndexs);
        }

        
        
        /// <summary>
        /// 获得密钥作为Sender的玩家不能解码
        /// </summary>
        public void OnGetCode(int[] codes)
        {
            pnlWord.ShowCode(codes);
        }
        
        /// <summary>
        /// 获得密钥作为Sender的玩家不能解码
        /// </summary>
        public void OnGetCodeButNoSender()
        {
            pnlWord.HideCode();
        }

        public void ConfirmCode()
        {
            playerUnit.DecodeNumberConfirm();
        }
    
    

    }
}


