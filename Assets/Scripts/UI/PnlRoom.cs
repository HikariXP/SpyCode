using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlRoom : PanelBase
{
    public Button BtnDisconnect;
    public Button BtnChangeTeam;
    public Button BtnReady;

    public GameObject playerDisplayPrefab;

    public Transform teamBlueArchor;
    public Transform teamRedArchor;

    //private GameNetworkManager gameNetworkManager;

    private void RegisterButtonClickCallBack()
    {
        BtnDisconnect.onClick.AddListener(PlayerDoDisconnect);
        BtnChangeTeam.onClick.AddListener(PlayerOnChangeTeam);
        BtnReady.onClick.AddListener(PlayerDoReady);
    }


    /// <summary>
    /// 刷新玩家显示
    /// </summary>
    public void RefreshTeamDisplay(List<PlayerUnit> currentPlayerList)
    {
        if (currentPlayerList.Count <= 0) return;

        ClearChilds(teamBlueArchor);
        ClearChilds(teamRedArchor);


        foreach (PlayerUnit pu in currentPlayerList)
        {
            if (pu.playerTeamIndex == 0)
            {
                var playerProfileDisplayUnit = Instantiate(playerDisplayPrefab, teamBlueArchor).GetComponent<PlayerInfoShow>();
                playerProfileDisplayUnit.txtPlayerName.text = pu.playerName;
                playerProfileDisplayUnit.txtPlayerSignature.text = pu.playerSignature;
                playerProfileDisplayUnit.ShowReady(pu.isReady);
            }
            else
            {
                var playerProfileDisplayUnit = Instantiate(playerDisplayPrefab, teamRedArchor).GetComponent<PlayerInfoShow>();
                playerProfileDisplayUnit.txtPlayerName.text = pu.playerName;
                playerProfileDisplayUnit.txtPlayerSignature.text = pu.playerSignature;
                playerProfileDisplayUnit.ShowReady(pu.isReady);
            }
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

    private void CreatePlayerDisplayUnit(Transform archorTransform)
    { 
        
    }

    private void PlayerDoDisconnect()
    { 
        //GameServerManager.Instance.Disconnect();
    }

    private void PlayerOnChangeTeam()
    {
        UIManager.instance.localPlayerUnit.ChangeTeam();
    }

    private void PlayerDoReady()
    {
        UIManager.instance.localPlayerUnit.SetReady();
    }

    public override void Reset()
    {
        
    }

    public override void Init()
    {
        //gameNetworkManager = UIManager.instance.GPN_Server;

        RegisterButtonClickCallBack();

        //gameNetworkManager.OnRoomPlayerAmountChange += RefreshTeamDisplay;
        //需要从新设计获得房间玩家数量变化的方式。
    }
}
