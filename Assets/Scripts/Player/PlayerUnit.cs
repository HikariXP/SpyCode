using System;
using System.Collections.Generic;
using Mirror;
using NetworkControl.GamePlayNetwork;
using NetworkControl.UI;
using UnityEngine;


public class PlayerUnit : NetworkBehaviour
{
    /// <summary>
    /// 玩家昵称
    /// </summary>
    [SyncVar]
    public string playerName;
    
    /// <summary>
    /// 玩家个性签名
    /// </summary>
    [SyncVar]
    public string playerSignature;
    
    /// <summary>
    /// 是否已经准备好游戏
    /// </summary>
    [SyncVar]
    public bool isReady;
    
    /// <summary>
    /// 玩家所属队伍Id
    /// TODO:需要重构相关获取行为。
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    //本地缓存的索引
    private List<int> wordIndexs;

    /// <summary>
    /// 所属队伍的引用，目前不该获取其
    /// </summary>
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
        Debug.Log($"[{nameof(PlayerUnit)}]OnStartClient");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        UnregisterToGPNPlay();
        Debug.Log($"[{nameof(PlayerUnit)}]OnStopClient");
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


    #region Word
    
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

    #region Decode

    //TODO:改成获取单独一个客户端则不需要对isLocalPlayer判断，还能提升安全性

    /// <summary>
    /// 客户端获取代码
    /// </summary>
    /// <param name="_">TargetRpc需要使用</param>
    /// <param name="codes"></param>
    [TargetRpc]
    public void Rpc_GPNPlaySetCode(NetworkConnectionToClient _, int[] codes)
    {
        UISystem.Instance.battleUI.NewTurnWithCode(codes);
        UISystem.Instance.battleUI.pnlRoundTips.EndWaitForEnemyMask();
    }

    /// <summary>
    /// 客户端刷新当前分数
    /// </summary>
    /// <param name="_">TargetRpc需要使用</param>
    /// <param name="successScore"></param>
    /// <param name="failScore"></param>
    [TargetRpc]
    public void Rpc_GPNPlayGetScore(NetworkConnectionToClient _, int successScore, int failScore)
    {
        UISystem.Instance.battleUI.pnlWord.RefreshScore(successScore, failScore);
        Debug.Log($"[{nameof(PlayerUnit)}]Rpc_GPNPlayGetScore({successScore}, {failScore})");
    }

    [ClientRpc]
    public void Rpc_GPNPlayGameOver()
    {
        // 这种代码在纯Server-Client中不需要，但是在Host-Client中需要
        if (!isLocalPlayer) return;
        UISystem.Instance.GPNPlay_SetToRoomUI();
    }

    //如果引入TargetRpc，则需要一如networkConnection的传参放到第一位
    [TargetRpc]
    public void Rpc_TeamMemberConfirmCode(NetworkConnectionToClient _, int[] codes)
    {
        UISystem.Instance.battleUI.pnlRoundTips.BeginWaitForEnemyMask(codes);
    }

    [TargetRpc]
    public void Rpc_TeamMemberCancelConfirm(NetworkConnectionToClient _)
    {
        UISystem.Instance.battleUI.pnlRoundTips.EndWaitForEnemyMask();
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
    public void DecodeNumberConfirm(int[] answerCodes)
    {
        GPNPlay.instance.PlayerConfirmCode(this ,answerCodes);
    }
    
    [Command]
    public void DecodeNumberCancel()
    {
        GPNPlay.instance.PlayerCancelCode(this);
    }
}