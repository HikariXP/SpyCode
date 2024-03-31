/*
 * Author: CharSui
 * Created On: 2023.08.19
 * Description: 战局相关UI的管理器
 */

using System;
using System.Collections.Generic;
using UI.GamePlayOnline;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NetworkControl.UI
{
    /// <summary>
    /// GamePlayOnline下使用的UI总管理器。
    /// </summary>
    public class UISystem : MonoBehaviour
    {
        private HashSet<IInitAndReset> uiElements = new HashSet<IInitAndReset>(64);
        
        public static UISystem Instance;

        private PlayerUnit localPlayerUnit => BattleHelper.LocalPlayerUnit;

        public GameObject PnlRoom;

        public GameObject PnlBattle;

        public PnlRoom roomUI;

        public PnlBattle battleUI;

        private void Awake()
        {
            Instance = this;
            
        }

        private void Start()
        {
            // UI_Init();
        }

        // private void UI_Init()
        // {
        //     if(uiElements.Count <= 0)return;
        //     
        //     foreach (var ui in uiElements)
        //     {
        //         ui.UI_Init();
        //     }
        // }
        //
        // private void UI_Reset()
        // {
        //     if(uiElements.Count <= 0)return;
        //     
        //     foreach (var ui in uiElements)
        //     {
        //         ui.UI_Reset();
        //     }
        // }

        /// <summary>
        /// 注册UISystem的控制(start之前操作)
        /// </summary>
        public void UI_RegisterIInit(IInitAndReset iir)
        {
            
        }
        
        /// <summary>
        /// 卸载UISystem的控制(start之前操作)
        /// </summary>
        public void UI_UnregisterIInit(IInitAndReset iir)
        {
            
        }

        public void RefreshRoomUIForce()
        {
            roomUI.RefreshShow();
        }

        public void OnPlayerConfirm(int[] answerCodes)
        {
            localPlayerUnit.DecodeNumberConfirm(answerCodes);
        }

        public void OnPlayerCancel()
        {
            localPlayerUnit.DecodeNumberCancel();
        }
        

        /// <summary>
        /// 进入房间模式
        /// </summary>
        public void GPNPlay_SetToRoomUI()
        {
            PnlRoom.SetActive(true);
            PnlBattle.SetActive(false);
        }

        /// <summary>
        /// 进入游玩模式
        /// </summary>
        public void GPNPlay_SetToPlayUI()
        {
            PnlRoom.SetActive(false);
            roomUI.Reset();
            PnlBattle.SetActive(true);
        }
        
        #if UNITY_EDITOR
        
        [Button]
        public void ResetUI_Room()
        {
            GPNPlay_SetToRoomUI();
        }
        
        [Button]
        public void ResetUI_Battle()
        {
            GPNPlay_SetToPlayUI();
        }
        
        #endif
    }
}

