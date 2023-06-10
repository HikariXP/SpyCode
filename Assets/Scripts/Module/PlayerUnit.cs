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
    public string playerSignature;

    #endregion

    #region �Ծ���Ϣ

    /// <summary>
    /// ����Ƿ��Ѿ�׼��
    /// </summary>
    [SyncVar]
    public bool isReady;

    /// <summary>
    /// ��Ҷ���,0�����ӡ�1�Ǻ�ӣ��޹ض���
    /// </summary>
    [SyncVar]
    public int playerTeamIndex;

    #endregion �Ծ���Ϣ

    /// <summary>
    /// ��ѡ׼��״̬
    /// </summary>
    [Command]
    public void SetReady()
    {
        isReady = !isReady;
    }
}
