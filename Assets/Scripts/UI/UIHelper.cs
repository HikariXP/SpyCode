using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��UI��ȡ�������
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
