
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
        public static UISystem Instance;

        private PlayerUnit localPlayerUnit;

        public GameObject PnlRoom;

        public GameObject PnlBattle;

        public PnlRoom roomUI;

        public PnlBattle battleUI;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayerSetup(PlayerUnit local)
        {
            localPlayerUnit = local;
            roomUI.Init(localPlayerUnit);
            battleUI.Init(localPlayerUnit);
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
            battleUI.Reset();
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

