
using NetworkControl.GamePlayNetwork;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlayOnline
{
    public class PnlRoom : MonoBehaviour
    {


        public Transform PlayerInfoViewTransform;

        public GameObject PlayerInfoCellPrefab;
        
        
        [Header("Button")]
        public Button BtnReady;

        public Button BtnChangeTeam;

        public Button BtnBack;

        public void Start()
        {
            BtnReady.onClick.AddListener(CMD_PlayerReady);
            BtnChangeTeam.onClick.AddListener(CMD_PlayerChangeTeam);
            BtnBack.onClick.AddListener(DisconnectAndBackToTitle);
        }

        private void Awake()
        {
            BtnReady.onClick.RemoveAllListeners();
            BtnChangeTeam.onClick.RemoveAllListeners();
            BtnBack.onClick.RemoveAllListeners();
        }

        public void RefreshShow()
        {
            ClearChilds(PlayerInfoViewTransform);
            
            var tempList = GPNPlay.instance.playerUnits;

            foreach (var playerUnit in tempList)
            {
                if(playerUnit==null)continue;
                var cell = Instantiate(PlayerInfoCellPrefab, PlayerInfoViewTransform).GetComponent<PlayerInfoRoomCell>();
                cell.RefreshInfo(playerUnit);
            }
        }

        private void ClearChilds(Transform parent)
        {
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
            }
        }

        public void Reset()
        {
            
        }

        #region Player Action

        private void CMD_PlayerReady()
        {
            BattleHelper.LocalPlayerUnit.Cmd_SetReady();
        }

        private void CMD_PlayerChangeTeam()
        {
            BattleHelper.LocalPlayerUnit.Cmd_ChangeTeam();
        }

        private void DisconnectAndBackToTitle()
        {
            
        }

        #endregion
    }
}

