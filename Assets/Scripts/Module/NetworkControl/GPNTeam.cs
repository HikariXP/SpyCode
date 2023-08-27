using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPNTeam
{
    public int teamIndex;

    private List<PlayerUnit> members = new List<PlayerUnit>();

    private int senderIndex;

    public GPNTeam()
    { }

    public PlayerUnit GetSenderPlayer()
    {
        var result = members[senderIndex];
        SetSenderIndex();

        if(result!=null)return result;
        else return null;
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
        }
    }

    public void Remove(PlayerUnit playerUnit) 
    {
        if (members.Contains(playerUnit))
        {
            members.Remove(playerUnit);
        }
    }

    public int Count()
    {
        return members.Count;
    }

    #endregion
}
