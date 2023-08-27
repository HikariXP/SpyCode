using Mirror;
using NetworkControl.GamePlayNetwork;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.GamePlayOnline;
using UnityEngine;
using static Mirror.SyncIDictionary<uint, PlayerUnit>;

namespace NetworkControl.UI
{
    /// <summary>
    /// GamePlayOnline下使用的UI总管理器。
    /// </summary>
    public class UISystem : MonoBehaviour
    {
        public static UISystem Instance;

        private PlayerUnit localPlayerUnit;

        public GameObject PnlRoom;

        public GameObject PnlPlay;

        public PnlRoom roomUI;

        public PnlBattle battleUI;

        private void Awake()
        {
            Instance = this;
        }

        //private void Start()
        //{
        //    GPNPlay.instance.playerUnits.Callback += RefreshRoomUI;
        //}

        //private void OnDestroy()
        //{
        //    GPNPlay.instance.playerUnits.Callback -= RefreshRoomUI;
        //}

        public void PlayerSetup(PlayerUnit local)
        {
            localPlayerUnit = local;
            roomUI.Init(localPlayerUnit);
            battleUI.Init(localPlayerUnit);
        }

        public void RefreshRoomUI(Operation op, uint key, PlayerUnit item)
        {
            //if(op == Operation.OP_SET)
            //{
            //    roomUI.RefreshShow();
            //}
        }

        public void RefreshRoomUIForce()
        {
            roomUI.RefreshShow();
        }

        /// <summary>
        /// 进入房间模式
        /// </summary>
        public void GPNPlay_SetToRoomUI()
        {
            PnlRoom.SetActive(true);
            PnlPlay.SetActive(false);
        }

        /// <summary>
        /// 进入游玩模式
        /// </summary>
        public void GPNPlay_SetToPlayUI()
        {
            PnlRoom.SetActive(false);
            PnlPlay.SetActive(true);
        }
    }
}

