using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器，这里负责处理一些由单个玩家发出的逻辑Command
/// </summary>
public class PlayerUnitCommander : NetworkBehaviour
{
    private PlayerUnit controlPlayerUnit;

    public override void OnStartLocalPlayer()
    {
        controlPlayerUnit = gameObject.GetComponent<PlayerUnit>();

        RegisterUI();
    }

    private void RegisterUI()
    {
        UIHelper.instance.localPlayerUnit = controlPlayerUnit;
        UIHelper.instance.localPlayerUnitCommander = this;

    }

    #region GamePlay相关操作

    /// <summary>
    /// 切换选中词语牌子
    /// </summary>
    [Command]
    public void OrderToChangeWordCard(int wordCardIndex)
    { 
        
    }

    /// <summary>
    /// 输入破译密码
    /// </summary>
    [Command]
    public void OrderToInputDecodeNumber(int number)
    { 
        
    }

    /// <summary>
    /// 确认输入的破译密码
    /// </summary>
    [Command]
    public void OrderToConfirmDecodeNumber()
    { 
    
    }


    #endregion

    //放回给房间相关管理去操控。
    //#region 房间相关操作

    ///// <summary>
    ///// 指挥启动连接
    ///// </summary>
    //public void OrderToConnect()
    //{ 

    //}

    //public void OrderToDisconnect()
    //{ 

    //}

    //#endregion
}
