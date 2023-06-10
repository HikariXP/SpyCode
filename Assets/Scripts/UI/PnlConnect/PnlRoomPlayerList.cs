using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家的信息展示，生成玩家预览到对应的队伍位置，没太大必要加入对象池管理，因为玩家总数不超过4个人,后期不超过6个人
/// </summary>
public class PnlRoomPlayerList : MonoBehaviour
{
    public Transform teamPlayerShowListArchor_A;
    public Transform teamPlayerShowListArchor_B;

    public GameObject playerInfoUnitPerfab;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        GameNetworkManager.Instance.OnPlayerAmountChange += RefreshCurrentState;
    }

    private void OnDisable()
    {
        GameNetworkManager.Instance.OnPlayerAmountChange -= RefreshCurrentState;
    }

    /// <summary>
    /// 根据现在GameNetworkManager的状况刷新房间玩家显示
    /// </summary>
    public void RefreshCurrentState(int playerAmount)
    {
        ClearChilds(teamPlayerShowListArchor_A);
        ClearChilds(teamPlayerShowListArchor_B);

        CreatePlayerInfoShowUnitToTransform();
    }

    private void ClearChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }


    private void CreatePlayerInfoShowUnitToTransform()
    {
        //求效率了，可重构
        foreach (KeyValuePair<Mirror.NetworkConnectionToClient, PlayerUnit> playerConnect in GameNetworkManager.Instance.playerList)
        {
            if (playerConnect.Value.playerTeamIndex == 0)
            {
                var tempPlayerInfoUnit = Instantiate(playerInfoUnitPerfab, teamPlayerShowListArchor_A);
                tempPlayerInfoUnit.GetComponent<PlayerInfoShow>().txtPlayerName.text = playerConnect.Value.playerName;
                tempPlayerInfoUnit.GetComponent<PlayerInfoShow>().txtPlayerSignature.text = playerConnect.Value.playerSignature;
            }
            else
            {
                var tempPlayerInfoUnit = Instantiate(playerInfoUnitPerfab, teamPlayerShowListArchor_B);
                tempPlayerInfoUnit.GetComponent<PlayerInfoShow>().txtPlayerName.text = playerConnect.Value.playerName;
                tempPlayerInfoUnit.GetComponent<PlayerInfoShow>().txtPlayerSignature.text = playerConnect.Value.playerSignature;
            }
        }
    }
}
