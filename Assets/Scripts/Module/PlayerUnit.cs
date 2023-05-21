using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������������Ԥ��
/// </summary>
public class PlayerUnit : NetworkBehaviour
{
    #region ������Ϣ

    /// <summary>
    /// �������
    /// </summary>
    [SyncVar]
    public string playerName;

    /// <summary>
    /// ��Ҹ���ǩ��
    /// </summary>
    [SyncVar]
    public string playerLabel;

    #endregion

    #region �Ծ���Ϣ

    /// <summary>
    /// ����Ƿ��Ѿ�׼��
    /// </summary>
    [SyncVar]
    public bool isReady;

    /// <summary>
    /// ��Ҷ���
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    #endregion �Ծ���Ϣ

    /// <summary>
    /// ����Ϊ׼��
    /// </summary>
    [Command]
    public void SetReady()
    {
        isReady = true;
    }
}
