using Mirror;
using NetworkControl.GamePlayNetwork;
using NetworkControl.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 局域网联机的玩家预设
/// Modifity:基于快速开发，将玩家存储数据以及玩家行为耦合。多个命令发送器虽然基于NetworkBehaviour，但是应该可以存在于同一个GameObject上。
/// </summary>
public class PlayerUnit : NetworkBehaviour
{
    #region 基础信息

    /// <summary>
    /// 玩家名字
    /// </summary>
    [SyncVar]
    public string playerName;

    /// <summary>
    /// 玩家个性签名
    /// </summary>
    [SyncVar]
    public string playerSignature;

    #endregion

    #region 对局信息

    /// <summary>
    /// 玩家是否已经准备
    /// </summary>
    [SyncVar]
    public bool isReady;

    /// <summary>
    /// 玩家队伍,0是蓝队、1是红队，无关队名
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    #endregion 对局信息

    #region 服务器初始化
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

    #endregion


    [Command]
    public void ChangeTeam()
    {
        playerTeamIndex = playerTeamIndex == 0 ? 1 : 0;
        GPNPlay.instance.PlayerStateRefresh();
    }

    /// <summary>
    /// 反选准备状态
    /// </summary>
    [Command]
    public void SetReady()
    {
        isReady = isReady ? false : true;
        GPNPlay.instance.PlayerStateRefresh();
    }


    #region PnlWord

    [Command]
    public void Word_ChangeWords()
    {

    }

    [Command]
    public void Word_Confirm()
    {

    }

    #endregion


    /// <summary>
    /// [客->服]告诉服务器当前玩家输入了哪个按键。
    /// </summary>
    /// <param name="number"></param>
    [Command]
    public void InputDecodeNumber(int number)
    {
        if (number > 9 || number < 0)
            return;



        //Tell Server Which Number Click
    }

    /// <summary>
    /// [客->服]对解码结果发起确认
    /// </summary>
    [Command]
    public void DecodeNumberConfirm(int[] numbers)
    { 
        
    }


    


    ///// <summary>
    ///// [服->特客]单独接收的密钥
    ///// </summary>
    //[TargetRpc]
    //[Obsolete("理论这个游戏需求想要实现，当前阶段不需要这个。")]
    //public void ReceiveCipher(int cipher)
    //{ 
        
    //}
}