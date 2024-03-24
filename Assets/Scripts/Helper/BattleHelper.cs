using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleHelper
{
    public static PlayerUnit LocalPlayerUnit { get; private set; }

    public static void SetLocalPlayer(PlayerUnit localPlayerUnit)
    {
        LocalPlayerUnit = localPlayerUnit;
    }
}
