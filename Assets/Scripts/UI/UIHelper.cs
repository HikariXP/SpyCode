using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 供UI获取本地玩家
/// </summary>
public class UIHelper : MonoBehaviour
{
    public static UIHelper instance;

    private void Awake()
    {
        instance = this;
    }

    public PlayerUnit localPlayerUnit;
}
