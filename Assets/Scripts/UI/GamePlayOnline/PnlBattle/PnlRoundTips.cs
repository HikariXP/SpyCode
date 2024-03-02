/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 用于处理一些置顶提示
 */

using System;
using System.Collections;
using System.Collections.Generic;
using NetworkControl.UI;
using UI.GamePlayOnline;
using UnityEngine;
using UnityEngine.UI;

public class PnlRoundTips : MonoBehaviour
{
    private PnlBattle m_PnlBattle;
    
    [Header("WaitForEnemyMask")]
    public GameObject waitForEnemyMask;
    //取消密码提交
    public Button btnCancel;

    private void Start()
    {
        btnCancel.onClick.AddListener(OnPlayerClickCancel);
    }

    private void OnDestroy()
    {
        btnCancel.onClick.RemoveAllListeners();
    }

    public void Init(PnlBattle pnlBattle)
    {
        m_PnlBattle = pnlBattle;
    }

    public void BeginWaitForEnemyMask(int[] teamCode)
    {
        waitForEnemyMask.SetActive(true);
    }

    public void EndWaitForEnemyMask()
    {
        waitForEnemyMask.SetActive(false);
    }


    private void OnPlayerClickCancel()
    {
        EndWaitForEnemyMask();
        UISystem.Instance.OnPlayerCancel();
    }
}
