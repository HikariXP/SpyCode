using System;
using System.Collections;
using System.Collections.Generic;
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
            pnlRoundTips.Init(this);
            // pnlWord.RefreshWordDisplay(playerUnit.team);
        }

        public void RefreshWordDisplay(List<int> wordIndexs)
        {
            pnlWord.RefreshWordDisplay(wordIndexs);
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
            //TODO:耦合性太强了，应该由服务器通知分数
            // pnlWord.RefreshScore(playerUnit.team.decodeSuccessScore,playerUnit.team.translateFailScore);
            
        }


        public void Reset()
        {
            
        }
    }
}


