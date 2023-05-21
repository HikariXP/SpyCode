using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战局管理器
/// </summary>
public class BattleManager : NetworkBehaviour
{
    [SyncVar]
    public List<GameObject> playerUnits;


}
