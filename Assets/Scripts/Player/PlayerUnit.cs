using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������������Ԥ��
/// Modifity:���ڿ��ٿ���������Ҵ洢�����Լ������Ϊ��ϡ�������������Ȼ����NetworkBehaviour������Ӧ�ÿ��Դ�����ͬһ��GameObject�ϡ�
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

    public event Action<SyncList<int>> OnLocalPlayerGetCode;

    [Command]
    public void ChangeTeam()
    {

    }

    /// <summary>
    /// ��ѡ׼��״̬
    /// </summary>
    [Command]
    public void SetReady()
    {
        
    }

    #region PnlWord

    [Command]
    public void Word_ChangeWords()
    {

    }

    [Command]
    public void Word_Confirm()
    {

    }

    #endregion


    /// <summary>
    /// [��->��]���߷�������ǰ����������ĸ�������
    /// </summary>
    /// <param name="number"></param>
    [Command]
    public void InputDecodeNumber(int number)
    {
        if (number > 9 || number < 0)
            return;



        //Tell Server Which Number Click
    }

    /// <summary>
    /// [��->��]�Խ���������ȷ��
    /// </summary>
    [Command]
    public void DecodeNumberConfirm(int[] numbers)
    { 
        
    }


    


    ///// <summary>
    ///// [��->�ؿ�]�������յ���Կ
    ///// </summary>
    //[TargetRpc]
    //[Obsolete("���������Ϸ������Ҫʵ�֣���ǰ�׶β���Ҫ�����")]
    //public void ReceiveCipher(int cipher)
    //{ 
        
    //}
}