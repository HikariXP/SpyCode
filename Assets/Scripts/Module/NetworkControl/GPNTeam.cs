using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GPNTeam
{
    public int teamIndex;

    private List<PlayerUnit> members = new List<PlayerUnit>();

    //实际字的内容客户端根据字典找
    private List<int> wordIndexs = new List<int>();

    private int senderIndex;

    private List<int> m_CurrentRoundDecode = new List<int>();

    private int m_Score;
    public int score => m_Score;

    public GPNTeam()
    { }

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
        for (int i = 0; i < members.Count; i++)
        {
            members[i].Rpc_TeamSetWordDisplay(wordIndexs);
        }
    }

    public void NewRound()
    {

    }

    /// <summary>
    /// 选择新的传译者
    /// </summary>
    /// <returns></returns>
    [Obsolete("意味着新回合开始，函数名并不能反映这一点")]
    public PlayerUnit GetSenderPlayer()
    {
        m_CurrentRoundDecode.Clear();
        
        var result = members[senderIndex];
        SetSenderIndex();

        if(result!=null)return result;
        return null;
    }

    private void SetSenderIndex()
    {
        senderIndex += 1;
        if(senderIndex>=members.Count)senderIndex = 0;
    }

    /// <summary>
    /// 目前版本确认后无法取消
    /// </summary>
    public void OnTeamMemberConfirmDecode(List<int> decode)
    {
        
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
