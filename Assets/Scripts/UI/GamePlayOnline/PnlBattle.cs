/*
 * Author: CharSui
 * Created On: 2023.08.19
 * Description: 战局UI
 */
using System.Collections.Generic;
using Module.WordSystem;
using UnityEngine;

namespace UI.GamePlayOnline
{
    public class PnlBattle : MonoBehaviour
    {
        public PnlWord pnlWord;

        public PnlRoundTips pnlRoundTips;
        
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
    }
}


