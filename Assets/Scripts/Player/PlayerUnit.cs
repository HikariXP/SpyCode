using System.Collections.Generic;
using Mirror;
using NetworkControl.GamePlayNetwork;
using NetworkControl.UI;
using UnityEngine;


public class PlayerUnit : NetworkBehaviour
{

    [SyncVar]
    public string playerName;
    
    [SyncVar]
    public string playerSignature;
    
    [SyncVar]
    public bool isReady;
    
    [SyncVar]
    public int playerTeamIndex;

    public List<int> wordIndexs;

    public GPNTeam team;



    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        UISystem.Instance.PlayerSetup(this);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        RegisterToGPNPlay();
        Debug.Log("[PlayerUnit]OnStartClient");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        UnregisterToGPNPlay();
        Debug.Log("[PlayerUnit]OnStopClient");
    }

    [Server]
    private void RegisterToGPNPlay()
    {
        GPNPlay.instance.AddPlayerUnit(this);

    }

    [Server]
    private void UnregisterToGPNPlay()
    {
        GPNPlay.instance.RemovePlayerUnit(this);
    }



    [Command]
    public void ChangeTeam()
    {
        playerTeamIndex = playerTeamIndex == 0 ? 1 : 0;
        GPNPlay.instance.PlayerStateRefresh();
    }


    [Command]
    public void SetReady()
    {
        isReady = !isReady;
        GPNPlay.instance.PlayerStateRefresh();
    }


    #region PnlWord
    
    [ClientRpc]
    public void Rpc_TeamSetWordDisplay(List<int> getWordIndexs)
    {
        if(!isLocalPlayer)return;
        wordIndexs = getWordIndexs;
        UISystem.Instance.battleUI.RefreshWordDisplay(wordIndexs);
    }

    [Command]
    public void Word_ChangeWords()
    {

    }

    [Command]
    public void Word_Confirm()
    {

    }

    #endregion

    #region PnlBattle

    [ClientRpc]
    public void Rpc_GPNPlaySetCode(int[] codes)
    {
        if (!isLocalPlayer)
        {
            UISystem.Instance.battleUI.OnGetCodeButNoSender();
            return;
        }
        UISystem.Instance.battleUI.OnGetCode(codes);
    }

    #endregion


    [Command]
    public void InputDecodeNumber(int number)
    {
        if (number > 9 || number < 0)
            return;



        //Tell Server Which Number Click
    }

    [Command]
    public void DecodeNumberConfirm()
    { 
        // 不应该直接告诉GPNPlay用户确认，应该告诉Team并且触发事件通知GPNPlay
        // GPNPlay.instance.PlayerConfirmCode();
    }
}