/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 用于处理一些置顶提示
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NetworkControl.UI;
using UI.GamePlayOnline;
using UnityEngine;
using UnityEngine.UI;

public class PnlRoundTips : MonoBehaviour, IInitAndReset
{
    [Header("WaitForEnemyMask")]
    public GameObject waitForEnemyMask;
    //Team Code Show
    public Text txtTeamCode;
    //取消密码提交
    public Button btnCancel;

    private StringBuilder _sb = new StringBuilder();
    private char dividesSymbol = '.';

    [SerializeField]
    private PnlTurnResult _pnlTurnResult;

    public void Awake()
    {
        // UISystem.Instance.UI_RegisterIInit(_pnlTurnResult);
        _pnlTurnResult.UI_Init();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(OnPlayerClickCancel);
    }

    /// <summary>
    /// 当此小回合结束后，显示5秒的赛局结果，并等待点击后关闭
    /// </summary>
    private void OnTurnEnd(TurnResult tr)
    {
        
    }

    private void OnDestroy()
    {
        btnCancel.onClick.RemoveAllListeners();
        _sb = null;
    }

    public void BeginWaitForEnemyMask(int[] teamCode)
    {
        waitForEnemyMask.SetActive(true);
        _sb.Clear();
        for (int i = teamCode.Length-1; i >= 0 ; i--)
        {
            _sb.Append(teamCode[i]);
            _sb.Append(dividesSymbol);
        }
        txtTeamCode.text = _sb.ToString();
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

    public void UI_Init()
    {
        
    }

    public void UI_Reset()
    {
        
    }
}
