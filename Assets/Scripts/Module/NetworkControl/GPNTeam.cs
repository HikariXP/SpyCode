/*
 * Author: CharSui
 * Created On: 2023.08.26
 * Description: 对于部分规则的实践确保，交由玩游戏的玩家。
 * Team的实际操作只在Server有，提供对玩家的访问以及快捷操作
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using NetworkControl.GamePlayNetwork;
using UnityEngine;

public class GPNTeam
{
    public int teamIndex;

    private List<PlayerUnit> members = new List<PlayerUnit>();

    /// <summary>
    /// 对于词库的词的索引,实际字的内容客户端根据字典找
    /// </summary>
    private List<int> wordIndexs = new List<int>();

    /// <summary>
    /// 队伍内部循环传译者
    /// </summary>
    private int senderIndex;

    /// <summary>
    /// 当前回合队伍存储的解码
    /// </summary>
    public int[] currentTurnDecode;

    public bool isConfirm;

    public int decodeSuccessScore = 0;

    public int translateFailScore = 0;

    /// <summary>
    /// 是否本回合
    /// </summary>
    public bool isSenderTurn;

    public GPNTeam()
    {
        Debug.Log($"[{nameof(GPNTeam)}]new team created with index: {teamIndex}");
    }

    [Server]
    public void SetWordIndex(int a,int b,int c,int d)
    {
        wordIndexs.Clear();
        wordIndexs.Add(a);
        wordIndexs.Add(b);
        wordIndexs.Add(c);
        wordIndexs.Add(d);
        RefreshTeamMemberWordDisplay();
    }

    private void RefreshTeamMemberWordDisplay()
    {
        foreach (var player in members)
        {
            player.Rpc_TeamSetWordDisplay(wordIndexs);
        }
    }
    
    /// <summary>
    /// 选择新的传译者
    /// </summary>
    /// <returns></returns>
    public void NewTurn(TurnInfo turnInfo)
    {
        Debug.Log($"队伍[{teamIndex}]获取密钥:{turnInfo.currentTurnCode}");
        
        // State set
        isConfirm = false;
        isSenderTurn = turnInfo.currentTurnCode != null;
        
        for (int i = 0; i < members.Count; i++)
        {
            var player = members[i];
            //当前回合传递者
            player.Rpc_GPNPlaySetCode(i == senderIndex ? turnInfo.currentTurnCode : null);
            player.Rpc_GPNPlayGetScore(turnInfo.successScore, turnInfo.failScore);
        }
        
        currentTurnDecode = null;

        //循环目标传译者的索引
        if (isSenderTurn)
        {
            SetToNextSenderIndex();
        }
    }

    /// <summary>
    /// 循环目标传译者的索引
    /// </summary>
    private void SetToNextSenderIndex()
    {
        senderIndex += 1;
        if(senderIndex>=members.Count)senderIndex = 0;
    }

    /// <summary>
    /// 队伍玩家确认代码,确认过就不会删除
    /// </summary>
    public void OnTeamMemberConfirmDecode(int[] code)
    {
        isConfirm = true;
        currentTurnDecode = code;
        foreach (var player in members)
        {
            player.Rpc_TeamMemberConfirmCode(currentTurnDecode);
        }
    }

    /// <summary>
    /// 队伍玩家取消确认代码
    /// </summary>
    public void OnTeamMemberCancelDecode()
    {
        isConfirm = false;
        currentTurnDecode = null;
        foreach (var player in members)
        {
            player.Rpc_TeamMemberCancelConfirm();
        }
    }

    #region Low Level

    public void Reset()
    {
        members.Clear();
        senderIndex = 0;
    }

    public void Add(PlayerUnit playerUnit)
    {
        if (!members.Contains(playerUnit))
        {
            members.Add(playerUnit);
            playerUnit.team = this;
        }
    }

    public void Remove(PlayerUnit playerUnit) 
    {
        if (members.Contains(playerUnit))
        {
            members.Remove(playerUnit);
            playerUnit.team = null;
        }
    }

    public int Count()
    {
        return members.Count;
    }

    #endregion
}
