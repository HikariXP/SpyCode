using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using System.Net;
using System.Linq;
using NetworkControl.UI;

namespace NetworkControl.GamePlayNetwork
{
    /// <summary>
    /// 尝试做成服务器端特有管理器。
    /// </summary>
    public class GPNPlay : NetworkBehaviour
    {
        public readonly SyncDictionary<uint, PlayerUnit> playerUnits = new SyncDictionary<uint, PlayerUnit>();

        //[SerializeField]
        //public readonly Dictionary<uint, PlayerUnit> playerUnits = new Dictionary<uint, PlayerUnit>();

        public readonly SyncList<GPNTeam> teams = new SyncList<GPNTeam>();

        public static GPNPlay instance;

        public readonly SyncList<int> code = new SyncList<int>();

        [Server]
        private void ResetBattleBase()
        {

        }

        private void Awake()
        {
            instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            ResetBattleBase();
        }

        #region Room

        [Server]
        public void PlayerStateRefresh()
        {
            RefreshRoomUI();

            var isMemberEnough = CheckTeamMemberCount();

            var isAllReady = CheckReadyState();

            if (isMemberEnough && isAllReady)
            {
                GameBegin();
            }
        }

        [ClientRpc]
        private void RefreshRoomUI()
        {
            UISystem.Instance.RefreshRoomUIForce();
        }

        private bool CheckTeamMemberCount()
        {
            var playersInRoom = playerUnits.Values;
            if(playersInRoom.Count<4)return false;

            int teamACount = 0;
            int teamBCount = 0;

            
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                if (playersInRoom.ElementAt(i).playerTeamIndex == 0)
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

        [Server]
        private bool CheckReadyState()
        { 
            var playersInRoom = playerUnits.Values;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                if (!playersInRoom.ElementAt(i).isReady)
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

            teams.Clear();

            //暂时做成固定两队生成
            teams.Add(CreateTeam(0));
            teams.Add(CreateTeam(1));
        }

        private void GameEnd()
        {
            //UI切换到Room
            UISystem.Instance.GPNPlay_SetToRoomUI();
        }

        private void SmallRound()
        {
            RefreshCode();
        }

        private void BigRound()
        { 
        
        }

        private void RefreshCode()
        { 
            code.Clear();
            CodeAddNumber(1, 5, 3);
        }

        //添加的时候就检查重复问题。
        private void CodeAddNumber(int minInclusive, int maxExclusive,int codeCount)
        {
            for (int i = 0; i < codeCount; i++)
            {
                bool isFinish = false;
                while (!isFinish)
                { 
                    var number = Random.Range(minInclusive, maxExclusive);
                    if (code.Contains(number)) continue;
                    else
                    { 
                        code.Add(number);
                        break;
                    }
                }
            }
            Debug.Log(code.ToString());
        }

        [ClientRpc]
        private void RpcPlayerUIChange()
        {
            //UI切换到GamePlay
            UISystem.Instance.GPNPlay_SetToPlayUI();
        }

        #endregion

        private GPNTeam CreateTeam(int teamIndex)
        {
            GPNTeam team = new GPNTeam();
            team.teamIndex = teamIndex;

            var playersInRoom = playerUnits.Values;
            for (int i = 0; i < playersInRoom.Count; i++)
            {
                var player = playersInRoom.ElementAt(i);
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
            var playerUnitNetId = playerUnit.netIdentity.netId;
            if (!playerUnits.ContainsKey(playerUnitNetId))
            {
                playerUnits.Add(playerUnitNetId, playerUnit);
                Debug.Log("[GPNPlay]Add: " + playerUnit.playerName);
                PlayerStateRefresh();

                ForceRefreshPlayerUnits();

                RefreshRoomUI();
            }
        }

        [Server]
        public void RemovePlayerUnit(PlayerUnit playerUnit)
        {
            var playerUnitNetId = playerUnit.netIdentity.netId;
            if (playerUnits.ContainsKey(playerUnitNetId))
            {
                playerUnits.Remove(playerUnitNetId);
                Debug.Log("[GPNPlay]Remove: " + playerUnit.playerName);
                PlayerStateRefresh();

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

            for (int i = 0; i < count; i++)
            {
                var tempKey = playerUnits.ElementAt(i).Key;
                if (playerUnits[tempKey] != null) 
                {
                    playerUnits[tempKey] = playerUnits[tempKey];
                }
            }
        }
    }

    public class GPNPlayWordBackup
    {

    }
}
