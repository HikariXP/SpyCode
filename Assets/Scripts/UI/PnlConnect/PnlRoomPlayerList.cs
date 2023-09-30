
using UnityEngine;

public class PnlRoomPlayerList : MonoBehaviour
{
    public Transform teamPlayerShowListArchor_A;
    public Transform teamPlayerShowListArchor_B;

    public GameObject playerInfoUnitPerfab;

    internal void Init()
    {

    }

    /// <summary>
    /// ��������GameNetworkManager��״��ˢ�·��������ʾ
    /// </summary>
    public void RefreshCurrentState()
    {
        Debug.Log("Client Refresh");

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
        //foreach (PlayerUnit pu in GameNetworkManager.Instance.playerUnits)
        //{
        //    if (pu.playerTeamIndex == 0)
        //    {
        //        var playerProfileDisplayUnit = Instantiate(playerInfoUnitPerfab, teamPlayerShowListArchor_A).GetComponent<PlayerInfoShow>();
        //        playerProfileDisplayUnit.txtPlayerName.text = pu.playerName;
        //        playerProfileDisplayUnit.txtPlayerSignature.text = pu.playerSignature;
        //    }
        //    else
        //    {
        //        var playerProfileDisplayUnit = Instantiate(playerInfoUnitPerfab, teamPlayerShowListArchor_B).GetComponent<PlayerInfoShow>();
        //        playerProfileDisplayUnit.txtPlayerName.text = pu.playerName;
        //        playerProfileDisplayUnit.txtPlayerSignature.text = pu.playerSignature;
        //    }
        //}

    }
}
