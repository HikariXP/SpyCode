using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoRoomCell : MonoBehaviour
{
    public Text TxtPlayerName;
    public Text TxtPlayerSignature;

    public Text TxtPlayerTeamIndex;

    public GameObject ReadyIcon;

    public void RefreshInfo(PlayerUnit unit)
    {
        TxtPlayerName.text = unit.playerName;
        TxtPlayerSignature.text = unit.playerSignature;
        TxtPlayerTeamIndex.text = unit.playerTeamIndex.ToString();
        ReadyIcon.SetActive(unit.isReady);
    }
}
