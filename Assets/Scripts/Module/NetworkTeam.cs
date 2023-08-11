using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTeam
{
    [SyncVar]
    public int teamIndex; 

    private SyncList<PlayerUnit> teamPlayer;

    public int playerCount => teamPlayer.Count;

    public bool isAllReady
    {
        get
        {
            for (int i = 0; i < teamPlayer.Count; i++)
            {
                if (!teamPlayer[i].isReady) return false;
            }
            return true;
        }
    }

    /// <summary>
    /// ����ָ����һ�����𴫵ݵ��ˡ�
    /// </summary>
    [SyncVar]
    private int playerIndex = 0;

    /// <summary>
    /// �ɷ�����ָ��Ϊ��ǰ����غϿ�ʼ,��ȡ�غ���Կ
    /// </summary>
    [Server]
    public void TeamRound(SyncList<int> roundCode)
    {
        //teamPlayer[playerIndex].Word_GetCode(roundCode);
    }

    [Server]
    public bool isPlayerUnitInTeam(PlayerUnit playerUnit)
    {
        return teamPlayer.Contains(playerUnit);
    }

    [Server]
    public void AddPlayerUnit(PlayerUnit playerUnit)
    {
        if (isPlayerUnitInTeam(playerUnit)) return;
        teamPlayer.Add(playerUnit);
    }

    [Server]
    public void RemovePlayerUnit(PlayerUnit playerUnit) 
    {
        if (!isPlayerUnitInTeam(playerUnit)) return;
        teamPlayer.Remove(playerUnit);
    }
}
