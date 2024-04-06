/*
 * Author: CharSui
 * Created On: 2024.03.31
 * Description: 用于显示当前回合的信息
 */
using System.Text;
using Module.EventManager;
using UnityEngine;
using UnityEngine.UI;

public struct TurnResult
{
    /// <summary>
    /// 是否破译成功
    /// </summary>
    public bool isSuccess;

    /// <summary>
    /// 当前小回合密码:1.1.4
    /// </summary>
    public int[] turnCode;
}

public class PnlTurnResult : MonoBehaviour, IInitAndReset
{
    [SerializeField]
    private GameObject success;
    
    [SerializeField]
    private GameObject fail;
    
    [SerializeField]
    private Animator _Animator;

    [Header("Button")] 
    [SerializeField]
    private Button _BtnCancel;

    [SerializeField] private float _BtnCancelShowTime = 5f;

    private Coroutine _BtnCancelShowCoroutine;

    [Header("Result")] 
    [SerializeField]
    private Text _TxtTurnCode;
    [SerializeField]
    private Text _TxtResult;

    private StringBuilder _sb = new StringBuilder();
    private char dividesSymbol = '.';
    
    private void Awake()
    {
        _sb = new StringBuilder();
        _BtnCancel.onClick.AddListener(Hide);
    }

    private void OnDestroy()
    {
        _BtnCancel.onClick.RemoveAllListeners();
        EventManager.instance.TryGetArgEvent<TurnResult>(EventDefine.BATTLE_TURN_END).Unregister(Show);
    }
    
    public void UI_Init()
    {
        EventManager.instance.TryGetArgEvent<TurnResult>(EventDefine.BATTLE_TURN_END).Register(Show);
    }

    public void UI_Reset()
    {
        // EventManager.instance.TryGetArgEvent<TurnResult>(EventDefine.BATTLE_TURN_END).Unregister(Show);
    }

    /// <summary>
    /// 展示上一回合的对局信息
    /// </summary>
    /// <param name="tr"></param>
    private void Show(TurnResult tr)
    {
        _Animator.Play("show");

        var codes = tr.turnCode;
        _sb.Clear();
        foreach (var code in codes)
        {
            _sb.Append(code);
            _sb.Append(dividesSymbol);
        }
        _TxtTurnCode.text = _sb.ToString();

        // TODO:适配多语言
        _TxtResult.text = tr.isSuccess ? "解码成功" : "解码失败";

        success.SetActive(tr.isSuccess);
        fail.SetActive(!tr.isSuccess);
        _BtnCancel.interactable = true;
        _BtnCancel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _BtnCancel.interactable = false;
        _Animator.Play("hide");
        _BtnCancel.gameObject.SetActive(false);
    }


    #if UNITY_EDITOR

    [Sirenix.OdinInspector.Button]
    public void Editor_Show()
    {
        var tr = new TurnResult()
        {
            turnCode = new []{1,2,3}
        };
        Show(tr);
    }

#endif
    
}
