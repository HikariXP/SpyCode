using Mirror;
using NetworkControl.GamePlayNetwork;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlayOnline
{
    public class PnlRoom : MonoBehaviour
    {
        public Button BtnReady;

        public Button BtnChangeTeam;

        private PlayerUnit playerUnit;

        public Transform PlayerInfoViewTransform;

        public GameObject PlayerInfoCellPrefab;

        public void Init(PlayerUnit localPlayerUnit)
        {
            playerUnit = localPlayerUnit;
        }

        public void Start()
        {
            BtnReady.onClick.AddListener(CMD_PlayerReady);
            BtnChangeTeam.onClick.AddListener(CMD_PlayerChangeTeam);
        }

        public void RefreshShow()
        {
            ClearChilds(PlayerInfoViewTransform);

            //经过验证，问题不是因为同步的速度问题，延时1秒没有作用
            var tempList = GPNPlay.instance.playerUnits;

            for (int i = 0; i < tempList.Count; i++)
            {
                var cell = Instantiate(PlayerInfoCellPrefab, PlayerInfoViewTransform).GetComponent<PlayerInfoRoomCell>();
                cell.RefreshInfo(tempList.ElementAt(i).Value);
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

        #region Player Action

        public void CMD_PlayerReady()
        {
            playerUnit.SetReady();
        }

        public void CMD_PlayerChangeTeam()
        {
            playerUnit.ChangeTeam();
        }

        #endregion
    }
}

