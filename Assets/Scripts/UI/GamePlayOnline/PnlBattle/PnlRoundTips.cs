/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 用于处理一些置顶提示
 */

using System.Text;
using Module.EventManager;
using NetworkControl.UI;
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

    public GameObject pnlWaitForDecoder;
    public GameObject pnlSenderSpeck;

    [SerializeField]
    private PnlTurnResult _pnlTurnResult;

    public void Awake()
    {
        // TODO:重构一个集中管理器处理。
        _pnlTurnResult.UI_Init();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(OnPlayerClickCancel);
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_TEAM_MASK).Register(ShowWaitForDecoderUI);
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_SPEAK).Register(ShowSenderSpeak);

    }

    private void OnDestroy()
    {
        btnCancel.onClick.RemoveAllListeners();
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_TEAM_MASK).Unregister(ShowWaitForDecoderUI);
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_SENDER_SPEAK).Unregister(ShowSenderSpeak);
        _sb = null;
    }

    /// <summary>
    /// 传递方等待对方提交密码
    /// </summary>
    private void ShowWaitForDecoderUI(bool isShow)
    {
        pnlWaitForDecoder.SetActive(isShow);
    }

    private void ShowSenderSpeak(bool isShow)
    {
        pnlSenderSpeck.SetActive(isShow);
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
