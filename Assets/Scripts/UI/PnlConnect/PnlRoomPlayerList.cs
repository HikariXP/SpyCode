using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ҵ���Ϣչʾ���������Ԥ������Ӧ�Ķ���λ�ã�û̫���Ҫ�������ع�����Ϊ�������������4����,���ڲ�����6����
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
    /// ��������GameNetworkManager��״��ˢ�·��������ʾ
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
        //��Ч���ˣ����ع�
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
