using System;
using System.Collections;
using System.Collections.Generic;
using Module.EventManager;
using UnityEngine;
using UnityEngine.UI;

public class PnlRoundResult : MonoBehaviour
{
    public Button BtnBack;

    public Color successColor;
    public Color failColor;

    public Image TipsBg;

    public Text TxtTitle;
    public Text TxtTips;

    public Animator _animator;

    private const string _animationKeyShow = "show";
    private const string _animationKeyHide = "hide";

    private const string successText = "胜利";
    private const string failText = "失败";

    private const string successTipsText = "成功瓦解敌人的诡计";
    private const string failTipsText = "别气馁，我们还会再战的";

    private bool isShowed = false;

    public void Awake()
    {
        BtnBack.onClick.AddListener(OnClickBack);
        RegisterEvent();
    }

    public void OnDestroy()
    {
        BtnBack.onClick.RemoveAllListeners();
        UnregisterEvent();
    }

    private void RegisterEvent()
    {
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_GAME_END).Register(Show);
        EventManager.instance.TryGetNoArgEvent(EventDefine.BATTLE_GAME_START).Register(Hide);
    }

    private void UnregisterEvent()
    {
        EventManager.instance.TryGetArgEvent<bool>(EventDefine.BATTLE_GAME_END).Unregister(Show);
        EventManager.instance.TryGetNoArgEvent(EventDefine.BATTLE_GAME_START).Register(Hide);
    }

    private void OnClickBack()
    {
        Hide();
    }

    private void Show(bool isSuccess)
    {
        if (isSuccess) ShowVictory();
        else ShowFail();

        isShowed = true;
    }

    private void Hide()
    {
        if(!isShowed) return;
        
        _animator.Play(_animationKeyHide);
        BtnBack.gameObject.SetActive(false);
    }

    private void ShowVictory()
    {
        BtnBack.gameObject.SetActive(true);
        TipsBg.color = successColor;
        _animator.Play(_animationKeyShow);
        // 文案
        TxtTitle.text = successText;
        TxtTips.text = successTipsText;
    }

    private void ShowFail()
    {
        BtnBack.gameObject.SetActive(true);
        TipsBg.color = failColor;
        _animator.Play(_animationKeyShow);
        // 文案
        TxtTitle.text = failText;
        TxtTips.text = failTipsText;
    }
    
    #if UNITY_EDITOR

    [Sirenix.OdinInspector.Button]
    public void TestShow(bool isSuccess)
    {
        Show(isSuccess);
    }
#endif
}
