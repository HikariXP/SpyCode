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

    /// <summary>
    /// 选择新的传译者
    /// </summary>
    /// <returns></returns>
    public PlayerUnit GetSenderPlayer()
    {
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
