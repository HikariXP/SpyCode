/*
 * Author: CharSui
 * Created On: 2023.08.19
 * Description: 战局UI
 */

using System;
using System.Collections.Generic;
using Module.WordSystem;
using UnityEngine;

namespace UI.GamePlayOnline
{
    public class PnlBattle : MonoBehaviour, IInitAndReset
    {
        public PnlWord pnlWord;

        public PnlRoundTips pnlRoundTips;

        private void Awake()
        {
            
        }

        private void OnDestroy()
        {
            UnregisterEvent();
        }
        
        public void UI_Init()
        {
            RegisterEvent();
        }

        public void UI_Reset()
        {
            
        }

        private void RegisterEvent()
        {
            EventManager.instance.TryGetNoArgEvent(EventDefine.BATTLE_PLAYER_CONFIRM_WORDLIST)
                .Register(OnLocalPlayerConfirmWordList);
        }

        private void UnregisterEvent()
        {
            EventManager.instance.TryGetNoArgEvent(EventDefine.BATTLE_PLAYER_CONFIRM_WORDLIST)
                .Unregister(OnLocalPlayerConfirmWordList);
        }

        public void RefreshWordDisplay(List<WordData> words)
        {
            pnlWord.RefreshWordDisplay(words);
        }

        /// <summary>
        /// TODO:EventManager重构
        /// </summary>
        public void OnLocalPlayerConfirmWordList()
        {
            pnlWord.BtnConfirmWordList.interactable = false;
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

        public void ResetToDefault()
        {
            
        }
    }
}


