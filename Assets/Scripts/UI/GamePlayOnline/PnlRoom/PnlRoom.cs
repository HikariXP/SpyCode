using System;
using NetworkControl.GamePlayNetwork;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlayOnline
{
    public class PnlRoom : MonoBehaviour
    {
        public Button BtnReady;

        public Button BtnChangeTeam;

        private PlayerUnit _playerUnit;

        public Transform PlayerInfoViewTransform;

        public GameObject PlayerInfoCellPrefab;

        public void Init(PlayerUnit localPlayerUnit)
        {
            _playerUnit = localPlayerUnit;
        }

        public void Start()
        {
            BtnReady.onClick.AddListener(CMD_PlayerReady);
            BtnChangeTeam.onClick.AddListener(CMD_PlayerChangeTeam);
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

        public void CMD_PlayerReady()
        {
            _playerUnit.Cmd_SetReady();
        }

        public void CMD_PlayerChangeTeam()
        {
            _playerUnit.Cmd_ChangeTeam();
        }

        #endregion
    }
}

