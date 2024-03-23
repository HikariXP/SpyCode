/*
 * Author: CharSui
 * Created On: 2023.08.05
 * Description: 管理几乎所有在线战局的东西(我不会啊操，只能这样先了)
 * 现在的写法非常狗屎，后续需要根据UI框架进行重构
 */

/*
 * 一些定义:
 * Turn : 小回合
 * Round : 回合
 * Code : 这一局传递者需要传递的密码
 * Decoder : 解码者
 * Sender : 传译者
 */

/*
 * 开始回合流程：
 * 服务器生成一个随机码，自己存一份，交给目标队伍一份
 * 等待双方提交答案
 * 核对答案并生成结果，对回合做判断
 * 循环
 */

/*
 * 尚未完成的功能：
 * 回合开始的密码没有经过重复判断：缓存历史代码，跳过出现过的代码
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using System.Net;
using System.Linq;
using Module.CommonUtil;
using Module.WordSystem;
using NetworkControl.UI;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace NetworkControl.GamePlayNetwork
{
    /// <summary>
    /// 尝试做成服务器端特有管理器。
    /// </summary>
    public class GPNPlay : NetworkBehaviour
    {
        public readonly SyncList<PlayerUnit> playerUnits = new SyncList<PlayerUnit>();

        private readonly SyncList<GPNTeam> _teams = new SyncList<GPNTeam>();
        
        private int m_CurrentTeamIndex = -1;

        public static GPNPlay instance;

        public readonly SyncList<int> code = new SyncList<int>();

        private int _turnCount = 0;

        private int _roundCount = 0;

        /// <summary>
        /// 破译成功的分数上上限
        /// </summary>
        private int _DecodeSuccessScore = 2;
        
        /// <summary>
        /// 传译失败的分数上上限
        /// </summary>
        private int _TranslateFailScore = 2;

        private WordLoader _wordLoader;

        private void Awake()
        {
            instance = this;

            _wordLoader = new WordLoaderJson();

            _wordLoader.Init();

            string context;
            
// #if UNITY_EDITOR
//             var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/ProjectAsset/WordLibrary/DecryptoStandard.json");
//             context = textAsset.text;
// #else
            //TODO:这里是异步的，应该做成异步加载的流程。
            var textAsset = Addressables.LoadAssetAsync<TextAsset>("WordLibrary_DecryptoStandard");
            textAsset.WaitForCompletion();
            context = textAsset.Result.text;
            
// #endif
            _wordLoader.Load(context);
        }

        [Server]
        private void ResetBattleBase()
        {
            var playersInRoom = playerUnits;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                playersInRoom.ElementAt(i).isReady = false;
            }
        }

        [Server]
        private void ResetUI()
        {
            var playersInRoom = playerUnits;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                var player = playersInRoom.ElementAt(i);
                player.isReady = false;
                player.Rpc_GPNPlayGameOver();
                // UISystem.Instance.GPNPlay_SetToRoomUI();
            }
        }

        #region Room

        [Server]
        public void OnPlayerRefreshState()
        {
            RefreshRoomUI();
            
            var isMemberEnough = CheckTeamMemberCount();

            var isAllReady = CheckReadyStateIsReadyForGame();

            if (isMemberEnough && isAllReady)
            {
                GameBegin();
            }
        }

        [Server]
        private void RefreshRoomUI()
        {
            foreach (var player in playerUnits)
            {
                player.Rpc_RefreshRoomUI();
            }
        }

        /// <summary>
        /// 检查两个队伍中的玩家数量是否足以开始游戏(双方满2人)
        /// </summary>
        /// <returns></returns>
        private bool CheckTeamMemberCount()
        {
            var playersInRoom = playerUnits;
            if(playersInRoom.Count<4)return false;
            // if(playersInRoom.Count<2)return false;

            int teamACount = 0;
            int teamBCount = 0;

            
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                if (playersInRoom[i].playerTeamIndex == 0)
                {
                    teamACount += 1;
                }
                else
                {
                    teamBCount += 1;
                }
            }
            if (teamACount == teamBCount)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查所有玩家的准备状况是否足以开始游戏
        /// </summary>
        /// <returns></returns>
        [Server]
        private bool CheckReadyStateIsReadyForGame()
        { 
            var playersInRoom = playerUnits;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                if (!playersInRoom[i].isReady)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Game

        [Server]
        private void GameBegin()
        {
            RpcPlayerUIChange();

            _teams.Clear();
            _turnCount = 0;

            // 暂时做成固定两队生成
            _teams.Add(CreateTeam(0));
            _teams.Add(CreateTeam(1));
            
            // TODO:固定词序
            InitializeTeamsWordIndex();

            m_CurrentTeamIndex = 0;
            
            NewRound();
        }

        /// <summary>
        /// TODO:完善随机序号和可换序号
        /// </summary>
        [Server]
        private void InitializeTeamsWordIndex()
        {
            var wordCount = _wordLoader.GetCount();
            var tempRandomCacher = new RandomCacher(wordCount);
            
            // TODO:重复的代码，记得修复
            // 这里需要获取一次战斗的所有词序，然后随机分配成两组可供使用
            // 需要一次性给队伍所有的Index。
            List<WordData> tempCacheWords = new List<WordData>(64);
            tempCacheWords.Clear();
            //每个队伍给十个词
            for (int i = 0; i < 10; i++)
            {
                if (tempRandomCacher.GetNumber(out int wordIndex))
                {
                    if (_wordLoader.TryGetWord(wordIndex, out WordData word))
                    {
                        tempCacheWords.Add(word);
                    }
                }
            }

            _teams[0].GetWordDatas(tempCacheWords);
            tempCacheWords.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (tempRandomCacher.GetNumber(out int wordIndex))
                {
                    if (_wordLoader.TryGetWord(wordIndex, out WordData word))
                    {
                        tempCacheWords.Add(word);
                    }
                }
            }
            _teams[1].GetWordDatas(tempCacheWords);
        }

        private void GameEnd()
        {
            //展示结算画面，确认后切换到UIRoom
            ResetBattleBase();
            ResetUI();
        }

        [Server]
        public void PlayerChangeWord(PlayerUnit changePlayer, int wordIndex)
        {
            var playerTeam = changePlayer.team;
            playerTeam.ChangeWordData(wordIndex);
        }

        /// <summary>
        /// 这里感觉有点曲折，后期可以看看让玩家经由GPNTeam进行发送
        /// </summary>
        /// <param name="answerPlayer"></param>
        /// <param name="playerAnswerCodes"></param>
        [Server]
        public void PlayerConfirmCode(PlayerUnit answerPlayer, int[] playerAnswerCodes)
        {
            var playerTeam = answerPlayer.team;
            playerTeam.OnTeamMemberConfirmDecode(playerAnswerCodes);

            CheckTurn();
        }

        [Server]
        public void PlayerCancelCode(PlayerUnit answerPlayer)
        {
            var playerTeam = answerPlayer.team;
            playerTeam.OnTeamMemberCancelDecode();
            
            CheckTurn();
        }

        /// <summary>
        /// 检查是否小回合结束
        /// </summary>
        [Server]
        private void CheckTurn()
        {
            //这里拓展让对方队伍获得提示
            if(!CheckIsNextTurn())return;

            //结算分数
            CheckTeamCode();
            
            m_CurrentTeamIndex += 1;
            if (m_CurrentTeamIndex >= _teams.Count)
            {
                //已经所有小组都遍历完了
                if (IsHaveWinner()) GameEnd();
                else NewRound();
            }
            else NewTurn();
        }

        /// <summary>
        /// 检查每个队伍的提交情况，是否需要切换到下一个小回合
        /// </summary>
        [Server]
        private bool CheckIsNextTurn()
        {
            //遍历所有队伍，如果有队伍还没提交则还没可以
            foreach (var team in _teams)
            {
                if (!team.isConfirm)
                {
                    return false;
                }
            }

            //所有队伍都Confirm则开局
            return true;
        }

        /// <summary>
        /// 结算队伍提交的密码
        /// </summary>
        [Server]
        private void CheckTeamCode()
        {
            var answer = code.ToArray();
            foreach (var team in _teams)
            {
                //TODO:需要优化
                var teamAnswerOrigin = team.currentTurnDecode;
                var teamAnswerReverse = teamAnswerOrigin.Reverse().ToArray();
                //需要注意，数组是引用类型，里面的值才是值类型
                var isAnswerCorrect = CheckArrayIsSame(teamAnswerReverse, answer);
                
                if (team.isSenderTurn)
                {
                    //队伍本回合是传译
                    if (!isAnswerCorrect)
                    {
                        team.translateFailScore += 1;
                        Debug.Log($"Team{team.teamIndex} - Sender Turn get 1 fail:\n" +
                                  $"code:{answer[0]},{answer[1]},{answer[2]}\n" +
                                  $"answer:{teamAnswerReverse[0]},{teamAnswerReverse[1]},{teamAnswerReverse[2]}");
                    }
                }
                else
                {
                    //队伍本回合是破译
                    if (isAnswerCorrect)
                    {
                        team.decodeSuccessScore += 1;
                        Debug.Log($"Team{team.teamIndex} - Decode Turn get 1 success:\n" +
                                  $"code:{answer[0]},{answer[1]},{answer[2]}\n" +
                                  $"answer:{teamAnswerReverse[0]},{teamAnswerReverse[1]},{teamAnswerReverse[2]}");
                    }
                }
            }

            #if UNITY_EDITOR
            foreach (var team in _teams)
            {
                Debug.Log($"[{nameof(GPNPlay)}]队伍[{team.teamIndex}]:破译成功:[{team.decodeSuccessScore}] - 传译失败:[{team.translateFailScore}]");
            }
            #endif
        }

        private bool CheckArrayIsSame(int[] arrayA, int[] arrayB)
        {
            if (arrayA.Length != arrayB.Length) return false;

            for (int i = 0; i < arrayA.Length; i++)
            {
                var aElement = arrayA[i];
                var bElement = arrayB[i];

                if (aElement != bElement) return false;
            }

            return true;
        }

        /// <summary>
        /// 新的小回合
        /// </summary>
        private void NewTurn()
        {
            RefreshCode();
            SetCodeToTeam(m_CurrentTeamIndex);
            _turnCount += 1;
        }

        /// <summary>
        /// 新的大回合
        /// </summary>
        private void NewRound()
        {
            _roundCount += 1;
            m_CurrentTeamIndex = 0;
            NewTurn();
        }

        /// <summary>
        /// 是否游戏已经结束(算分是否达到阈值)
        /// </summary>
        /// <returns></returns>
        private bool IsHaveWinner()
        {
            foreach (var team in _teams)
            {
                Debug.Log( $"[Team{team.teamIndex}]:Decode:{team.decodeSuccessScore} - fail:{team.translateFailScore}");
                if (team.decodeSuccessScore >= _DecodeSuccessScore)
                {
                    // winnerTeam = team;
                    return true;
                }

                if (team.translateFailScore >= _TranslateFailScore)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOverTurnCount()
        {
            if (_turnCount > 12) return true;
            return false;
        }

        /// <summary>
        /// 生成不重复的密码
        /// TODO:需要优化，目前跟SyncList耦合
        /// </summary>
        /// <param name="minInclusive">最小值(包含)</param>
        /// <param name="maxExclusive">最大值(不包含)</param>
        /// <param name="codeCount">生成密码数</param>
        private void RefreshCode(int minInclusive = 1, int maxExclusive = 5,int codeCount = 3)
        {
            code.Clear();
            var numberRange = maxExclusive - minInclusive;
            if (codeCount > numberRange)
            {
                Debug.LogError($"[{nameof(GPNPlay)}]密钥刷新入参错误，检查范围");
                return;
            }

            for (int i = 0; i < codeCount; i++)
            {
                while (true)
                { 
                    //1、获取随机数
                    var number = UnityEngine.Random.Range(minInclusive, maxExclusive);
                    //2、如果不重复则添加
                    if (!code.Contains(number))
                    {
                        code.Add(number);
                        break;
                    }
                }
            }
        }
        
        
        /// <summary>
        /// 不再将密钥传递给准确用户，而是通过Team去选择
        /// </summary>
        /// <param name="senderTeamIndex"></param>
        [Server]
        private void SetCodeToTeam(int senderTeamIndex)
        {
            foreach (var team in _teams)
            {
                if (team.teamIndex == senderTeamIndex)
                {
                    //本回合传递队伍
                    var codeArray = code.ToArray();
                    var turnInfo = new TurnInfo()
                    {
                        currentTurnCode = codeArray,
                        successScore = team.decodeSuccessScore,
                        failScore = team.translateFailScore
                    };
                    team.NewTurn(turnInfo);
                }
                else
                {
                    var turnInfo = new TurnInfo()
                    {
                        currentTurnCode = null,
                        successScore = team.decodeSuccessScore,
                        failScore = team.translateFailScore
                    };
                    //非本回合队伍
                    team.NewTurn(turnInfo);
                }
            }
        }

        [ClientRpc]
        private void RpcPlayerUIChange()
        {
            //UI切换到GamePlay
            UISystem.Instance.GPNPlay_SetToPlayUI();
        }

        #endregion

        #region Team

        private GPNTeam CreateTeam(int teamIndex)
        {
            GPNTeam team = new GPNTeam();
            team.teamIndex = teamIndex;

            var playersInRoom = playerUnits;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                var player = playersInRoom[i];
                if (player.playerTeamIndex == teamIndex)
                {
                    team.Add(player);
                }
                else
                { 
                    team.Remove(player);
                }
            }
            
            return team;
        }

        [Server]
        public void AddPlayerUnit(PlayerUnit playerUnit)
        {
            if (!playerUnits.Contains(playerUnit))
            {
                playerUnits.Add(playerUnit);
                Debug.Log("[GPNPlay]Add: " + playerUnit.playerName);
                ForceRefreshPlayerUnits();
                RefreshRoomUI();
            }
        }

        [Server]
        public void RemovePlayerUnit(PlayerUnit playerUnit)
        {
            if (playerUnits.Contains(playerUnit))
            {
                playerUnits.Remove(playerUnit);
                Debug.Log("[GPNPlay]Remove: " + playerUnit.playerName);
                ForceRefreshPlayerUnits();
                RefreshRoomUI();
            }
        }

        /// <summary>
        /// SyncDictionary核心同步逻辑(以下简写SD)
        /// 由于SD的同步只会增量同步，也就是第二个客户端没法获得StartClient之前的已存在的服务器信息
        /// 例子：服务器有两个玩家，但是作为客户端加入的，2P玩家，默认情况下没有被同步到1号位玩家的数据，所以1号位的Value为Null
        /// 此函数将所有位置的数据都强制让Mirror刷新，原地TP让Mirror认为确实出现了变更，每个位置都出现变更。
        /// 修改需谨慎
        /// </summary>
        [Server]
        private void ForceRefreshPlayerUnits()
        {
            //核心欺骗逻辑
            //playerUnits[newNetId] = playerUnits[newNetId];

            var count = playerUnits.Count;
            Debug.Log("[GPNPlay]Count:" + count);

            for (int i = 0; i < count-1; i++)
            {
                // var tempKey = playerUnits[i];
                // var tempKey2 = playerUnits[i+1];
                // if (tempKey != null)
                // {
                //     
                // }
                (playerUnits[i], playerUnits[i+1]) = (playerUnits[i+1], playerUnits[i]);
            }
        }

        #endregion
    }

    public struct TurnInfo
    {
        // public bool inTurn; 暂且不用，以密码块是否有内容为是否回合内判断。
        public int[] currentTurnCode;
        public int successScore;
        public int failScore;
    }

    public static class GPNPlayWordBackup
    {
        public static string[] WordBackup = new []
        {
            "鸡",
            "薄荷",
            "烤肉",
            "大逃杀",
            "猪",
            "非洲",
            "塔塔开",
            "我浪",
            "抽筋",
            "闭嘴",
            "吃大便",
            "沟罢",
            "有趣的傻逼",
            "测试用力1",
            "测试永利2",
            "侧室用例3"
        };
    }

}
