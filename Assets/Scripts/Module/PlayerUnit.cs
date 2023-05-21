using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 局域网联机的玩家预设
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
    public string playerLabel;

    #endregion

    #region 对局信息

    /// <summary>
    /// 玩家是否已经准备
    /// </summary>
    [SyncVar]
    public bool isReady;

    /// <summary>
    /// 玩家队伍
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    #endregion 对局信息

    /// <summary>
    /// 设置为准备
    /// </summary>
    [Command]
    public void SetReady()
    {
        isReady = true;
    }
}
