using System;
using System.Collections;
using System.Collections.Generic;
using Module.WordSystem;
using UnityEngine;

namespace UI.GamePlayOnline
{
    public class PnlBattle : MonoBehaviour
    {
        private PlayerUnit playerUnit;
    
        public PnlWord pnlWord;

        public PnlDecode pnlDecode;

        public PnlRoundTips pnlRoundTips;

        public void Init(PlayerUnit localPlayerUnit)
        {
            playerUnit = localPlayerUnit;
        }

        public void RefreshWordDisplay(List<WordData> words)
        {
            pnlWord.RefreshWordDisplay(words);
        }
        
        /// <summary>
        /// 获得密钥作为Sender的玩家不能解码
        /// </summary>
        public void NewTurnWithCode(int[] codes)
        {
            if (codes == null)
            {
                pnlWord.HideCode();
                return;
            }

            pnlWord.ShowCode(codes);
        }


        public void Reset()
        {
            
        }
    }
}


