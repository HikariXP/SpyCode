/*
 * Author: CharSui
 * Created On: 2023.08.26
 * Description: 对于部分规则的实践确保，交由玩游戏的玩家。
 * Team的实际操作只在Server有，提供对玩家的访问以及快捷操作
 */

/*
 * 重新确认了逻辑：当队伍处于破译方的时候，不需要显示自己的词语，当队伍处于传译方的时候才需要显示自己的词语。
 * 总体队伍的阶段：
 * 1、选词阶段
 * 2、破译-传递阶段
 * 3、结算阶段
 */

using System.Collections.Generic;
using Mirror;
using Module.WordSystem;
using NetworkControl.GamePlayNetwork;
using UnityEngine;

namespace Module.NetworkControl
{
    public class GPNTeam
    {
        public int teamIndex;

        private Queue<PlayerUnit> _members = new Queue<PlayerUnit>();
        
        /// <summary>
        /// 服务器给的10个词语
        /// </summary>
        private Queue<WordData> _wordDatas = new Queue<WordData>(10);

        private List<WordData> _wordSelected = new List<WordData>(4);

        /// <summary>
        /// 当前回合队伍存储的解码
        /// </summary>
        public int[] currentTurnDecode;

        /// <summary>
        /// 是否确认提交的代码
        /// </summary>
        public bool isDecodeConfirm;

        public int decodeSuccessScore = 0;

        public int translateFailScore = 0;

        /// <summary>
        /// 用于选词阶段，队伍是否两个玩家都确认词语
        /// </summary>
        public bool wordConfirm;

        /// <summary>
        /// 本回合
        /// </summary>
        public bool isSenderTurn;

        public GPNTeam()
        {
            Debug.Log($"[{nameof(GPNTeam)}]new team created with index: {teamIndex}");
        }

        #region 选词阶段

        [Server]
        public void GetWordDatas(List<WordData> wordDatas)
        {
            if (wordDatas == null || wordDatas.Count == 0)
            {
                Debug.LogError($"[{nameof(GPNTeam)}]wordData is wrong");
                return;
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
        public void OnPlayerCmdChangeWordData(int wordIndex)
        {
            if(wordConfirm)return;
            
            if(wordIndex < 0 || wordIndex > 3)return;

            var previousWord = _wordSelected[wordIndex];

            var nextWord = _wordDatas.Dequeue();

            _wordSelected[wordIndex] = nextWord;
        
            _wordDatas.Enqueue(previousWord);

            RefreshTeamMemberWordDisplay();
        }

        /// <summary>
        /// 队伍中一名玩家确认词库：通常是队长(一号位)
        /// </summary>
        [Server]
        public void OnPlayerCmdConfirmWordList()
        {
            CheckIsAllTeamMemberConfirmWordList();
        }

        [Server]
        private void CheckIsAllTeamMemberConfirmWordList()
        {
            foreach (var player in _members)
            {
                if (!player.isConfirmWordList)return;
            }
            
            wordConfirm = true;
            OnTeamEndWordSelect();
        }

        [Server]
        private void RefreshTeamMemberWordDisplay()
        {
            foreach (var player in _members)
            {
                player.Rpc_TeamSetWordDisplay(_wordSelected);
            }
        }

        /// <summary>
        /// 结束选词阶段
        /// </summary>
        [Server]
        private void OnTeamEndWordSelect()
        {
            GPNPlay.instance.CheckTeamWordListConfirm();
        }

        [Server]
        public void OnAllConfirmWordSelect()
        {
            foreach (var member in _members)
            {
                member.Rpc_AllTeamEndWordSelect(member.connectionToClient);
            }
        }


        #endregion 选词阶段
    
        /// <summary>
        /// 选择新的传译者
        /// </summary>
        /// <returns></returns>
        [Server]
        public void NewTurn(TurnInfo turnInfo)
        {
            Debug.Log($"队伍[{teamIndex}]获取密钥:{turnInfo.currentTurnCode}");
            
            isDecodeConfirm = false;
            isSenderTurn = turnInfo.currentTurnCode != null;
            currentTurnDecode = null;
            
            // 给所有玩家刷新UI
            foreach (var member in _members)
            {
                var memberConnectionToClient = member.connectionToClient;
                member.Rpc_GPNPlaySetCode(memberConnectionToClient, null);
                member.Rpc_GPNPlayGetScore(memberConnectionToClient, turnInfo.successScore, turnInfo.failScore);
            }

            var currentTurnCode = turnInfo.currentTurnCode;
            if(currentTurnCode == null || currentTurnCode.Length == 0) return;
            SetCodeToSender(currentTurnCode);
        }

        /// <summary>
        /// 获取下一个对内玩家且发送密钥让其成为Sender
        /// </summary>
        /// <param name="turnCode"></param>
        private void SetCodeToSender(int[] turnCode)
        {
            // 从队列给下一个Sender发送密码
            if (!TryGetNextSender(out var nextSender))
            {
                Debug.LogError($"[{nameof(GPNTeam)}]Get next Sender Player Failed");
                return;
            }

            var connectionToClient = nextSender.connectionToClient;
            nextSender.Rpc_GPNPlaySetCode(connectionToClient, turnCode);
        }

        private bool TryGetNextSender(out PlayerUnit nextSender)
        {
            if (_members == null)
            {
                nextSender = null;
                Debug.LogError($"[{nameof(GPNTeam)}]members is null");
                return false;
            }

            if (_members.Count <= 0)
            {
                nextSender = null;
                Debug.LogError($"[{nameof(GPNTeam)}]members count <= 0");
                return false;
            }

            nextSender = _members.Dequeue();
            _members.Enqueue(nextSender);
            return true;
        }

        /// <summary>
        /// 队伍玩家确认代码,确认过就不会删除
        /// </summary>
        public void OnTeamMemberConfirmDecode(int[] code)
        {
            isDecodeConfirm = true;
            currentTurnDecode = code;
            foreach (var player in _members)
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
            isDecodeConfirm = false;
            currentTurnDecode = null;
            foreach (var player in _members)
            {
                var connectionToClient = player.netIdentity.connectionToClient;
                player.Rpc_TeamMemberCancelConfirm(connectionToClient);
            }
        }

        #region Low Level

        public void Reset()
        {
            currentTurnDecode = null;
            isDecodeConfirm = false;
            wordConfirm = false;
            isSenderTurn = false;

            foreach (var member in _members)
            {
                member.isReady = false;
                member.isConfirmWordList = false;
            }
        }

        public void Add(PlayerUnit playerUnit)
        {
            if (!_members.Contains(playerUnit))
            {
                _members.Enqueue(playerUnit);
                playerUnit.team = this;
            }
        }

        public void Remove(PlayerUnit playerUnit) 
        {
            // if (members.Contains(playerUnit))
            // {
            //     members.Remove(playerUnit);
            //     playerUnit.team = null;
            // }

            List<PlayerUnit> tempCache = new List<PlayerUnit>();
            while (_members.Count > 0)
            {
                tempCache.Add(_members.Dequeue());
            }
            _members.Clear();
            tempCache.Remove(playerUnit);

            foreach (var playerCached in tempCache)
            {
                _members.Enqueue(playerCached);
            }
        }

        #endregion
    }
}
