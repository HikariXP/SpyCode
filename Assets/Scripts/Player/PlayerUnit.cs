using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Module.EventManager;
using Module.NetworkControl;
using Module.WordSystem;
using NetworkControl.GamePlayNetwork;
using NetworkControl.UI;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SyncVar] 
    public bool isConfirmWordList = false;
    
    /// <summary>
    /// 玩家所属队伍Id
    /// TODO:需要重构相关获取行为。
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    /// <summary>
    /// 所属队伍的引用，对于其用法需要再考虑
    /// </summary>
    public GPNTeam team;



    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        BattleHelper.SetLocalPlayer(this);
        RegisterToGPNPlay();
    }

    public override void OnStopServer()
    {
        UnregisterToGPNPlay();
        Debug.Log($"[{nameof(PlayerUnit)}]OnStopServer");
    }

    [Command]
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
    public void Cmd_ChangeTeam()
    {
        playerTeamIndex = playerTeamIndex == 0 ? 1 : 0;
        GPNPlay.instance.OnPlayerRefreshState();
    }


    [Command]
    public void Cmd_SetReady()
    {
        isReady = !isReady;
        GPNPlay.instance.OnPlayerRefreshState();
    }

    [ClientRpc]
    public void Rpc_RefreshRoomUI()
    {
        if(!isLocalPlayer)return;
        // ClientRpc即使执行顺序是对的，但是Sync的数据却不一定能几时同步
        // 或许最好的方式是通过Sync的数值改变事件进行回调
        StartCoroutine(waitEndFrame());
    }

    private IEnumerator waitEndFrame()
    {
        yield return new WaitForSeconds(0.05f);
        UISystem.Instance.RefreshRoomUIForce();
    }


    #region Word

    [Command]
    public void Cmd_PlayerChangeWord(int wordIndex)
    {
        GPNPlay.instance.PlayerChangeWord(this, wordIndex);
    }

    /// <summary>
    /// 玩家请求服务器确认代码
    /// </summary>
    [Command]
    public void Cmd_PlayerConfirmWordList()
    {
        GPNPlay.instance.PlayerConfirmWordList(this);
    }

    [TargetRpc]
    public void TargetRpc_OnPlayerConfirmWordList(NetworkConnectionToClient _)
    {
        if (isConfirmWordList)
        {
            EventManager.instance.TryGetNoArgEvent(EventDefine.BATTLE_PLAYER_CONFIRM_WORDLIST).Notify();
        }
        UISystem.Instance.battleUI.OnLocalPlayerConfirmWordList();
    }

    [TargetRpc]
    public void Rpc_AllTeamEndWordSelect(NetworkConnectionToClient _)
    {
        UISystem.Instance.battleUI.pnlWord.OnTeamEndWordSelected();
    }

    [ClientRpc]
    public void Rpc_TeamSetWordDisplay(List<WordData> words)
    {
        if(!isLocalPlayer)return;
        UISystem.Instance.battleUI.RefreshWordDisplay(words);
    }

    #endregion

    #region Decode

    [TargetRpc]
    public void Rpc_HideWaitForEnemyDecode(NetworkConnectionToClient _)
    {
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_TEAM_MASK).Notify(false);
    }
    
    [TargetRpc]
    public void Rpc_ShowWaitForEnemyDecode(NetworkConnectionToClient _)
    {
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_TEAM_MASK).Notify(true);
    }
    
    [TargetRpc]
    public void Rpc_HideSenderSpeckMask(NetworkConnectionToClient _)
    {
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_SPEAK).Notify(false);
    }

    
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
    public void DecodeNumberConfirm(int[] answerCodes)
    {
        GPNPlay.instance.PlayerConfirmCode(this ,answerCodes);
    }
    
    [Command]
    public void DecodeNumberCancel()
    {
        GPNPlay.instance.PlayerCancelCode(this);
    }

    [TargetRpc]
    public void TargetRpc_OnTurnEnd(NetworkConnectionToClient _, TurnResult tr)
    {
        EventManager.instance.TryGetArgEvent<TurnResult>(EventDefine.BATTLE_TURN_END).Notify(tr);
    }
}