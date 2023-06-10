using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ����������︺����һЩ�ɵ�����ҷ������߼�Command
/// </summary>
public class PlayerUnitCommander : NetworkBehaviour
{
    private PlayerUnit controlPlayerUnit;

    public override void OnStartLocalPlayer()
    {
        controlPlayerUnit = gameObject.GetComponent<PlayerUnit>();

        RegisterUI();
    }

    private void RegisterUI()
    {
        UIHelper.instance.localPlayerUnit = controlPlayerUnit;
        UIHelper.instance.localPlayerUnitCommander = this;

    }

    #region GamePlay��ز���

    /// <summary>
    /// �л�ѡ�д�������
    /// </summary>
    [Command]
    public void OrderToChangeWordCard(int wordCardIndex)
    { 
        
    }

    /// <summary>
    /// ������������
    /// </summary>
    [Command]
    public void OrderToInputDecodeNumber(int number)
    { 
        
    }

    /// <summary>
    /// ȷ���������������
    /// </summary>
    [Command]
    public void OrderToConfirmDecodeNumber()
    { 
    
    }


    #endregion

    //�Żظ�������ع���ȥ�ٿء�
    //#region ������ز���

    ///// <summary>
    ///// ָ����������
    ///// </summary>
    //public void OrderToConnect()
    //{ 

    //}

    //public void OrderToDisconnect()
    //{ 

    //}

    //#endregion
}
