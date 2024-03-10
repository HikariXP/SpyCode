/*
 * Author: CharSui
 * Created On: 2023.08.26
 * Description: 对于部分规则的实践确保，交由玩游戏的玩家。
 * Team的实际操作只在Server有，提供对玩家的访问以及快捷操作
 */
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mirror;
using Module.WordSystem;
using NetworkControl.GamePlayNetwork;
using UnityEngine;

public class GPNTeam
{
    public int teamIndex;

    private List<PlayerUnit> members = new List<PlayerUnit>();

    /// <summary>
    /// 对于词库的词的索引,实际字的内容客户端根据字典找
    /// </summary>
    [Obsolete]
    private List<int> wordIndexs = new List<int>();

    /// <summary>
    /// 队伍内部循环传译者
    /// </summary>
    private int senderIndex;

    private Queue<WordData> _wordDatas = new Queue<WordData>(10);

    private List<WordData> _wordSelected = new List<WordData>(4);

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

    [Obsolete]
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

    [Server]
    public void GetWordDatas(List<WordData> wordDatas)
    {
        if (wordDatas == null)
        {
            Debug.LogError($"[{nameof(GPNTeam)}]wordData is null");
        }

        if (wordDatas.Count == 0)
        {
            Debug.LogError($"[{nameof(GPNTeam)}]wordData is count 0");
        }

        foreach (var wordData in wordDatas)
        {
            _wordDatas.Enqueue(wordData);
        }

        for (int i = 0; i < 4; i++)
        {
            var item = _wordDatas.Dequeue();
            _wordSelected.Add(item);
        }
        RefreshTeamMemberWordDisplay();
    }

    /// <summary>
    /// 队伍内玩家切换词语
    /// </summary>
    /// <param name="wordIndex"></param>
    [Server]
    public void ChangeWordData(int wordIndex)
    {
        if(wordIndex < 0 || wordIndex > 3)return;

        var previousWord = _wordSelected[wordIndex];

        var nextWord = _wordDatas.Dequeue();

        _wordSelected[wordIndex] = nextWord;
        
        _wordDatas.Enqueue(previousWord);

        RefreshTeamMemberWordDisplay();
    }
    
    [Server]
    private void RefreshTeamMemberWordDisplay()
    {
        foreach (var player in members)
        {
            player.Rpc_TeamSetWordDisplay(_wordSelected);
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
            var connectionToClient = player.connectionToClient;
            //当前回合传递者
            player.Rpc_GPNPlaySetCode(connectionToClient, i == senderIndex ? turnInfo.currentTurnCode : null);
            player.Rpc_GPNPlayGetScore(connectionToClient, turnInfo.successScore, turnInfo.failScore);
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
            var connectionToClient = player.netIdentity.connectionToClient;
            player.Rpc_TeamMemberConfirmCode(connectionToClient, currentTurnDecode);
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
            var connectionToClient = player.netIdentity.connectionToClient;
            player.Rpc_TeamMemberCancelConfirm(connectionToClient);
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
