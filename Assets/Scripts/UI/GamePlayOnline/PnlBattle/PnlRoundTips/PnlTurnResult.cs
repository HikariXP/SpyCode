using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct TurnResult
{
    /// <summary>
    /// 是否传递方
    /// </summary>
    public bool isSender;
        
    /// <summary>
    /// 是否破译成功
    /// </summary>
    public bool isSuccess;

    /// <summary>
    /// 当前小回合密码:1.1.4
    /// </summary>
    public int[] turnCode;
}

public class PnlTurnResult : MonoBehaviour
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
    
    private void Awake()
    {
        _BtnCancel.onClick.AddListener(Hide);
    }

    private void OnDestroy()
    {
        _BtnCancel.onClick.RemoveAllListeners();
    }

    public void Show(TurnResult tr)
    {
        if (_BtnCancelShowCoroutine != null)
        {
            StopCoroutine(_BtnCancelShowCoroutine);
        }
        _Animator.Play("show");

        StartCoroutine(E_BtnCancelShowCoroutine());
    }

    private IEnumerator E_BtnCancelShowCoroutine()
    {
        yield return new WaitForSeconds(_BtnCancelShowTime);
        _BtnCancel.interactable = true;
    }

    private void Hide()
    {
        _BtnCancel.interactable = false;
        _Animator.Play("hide");
    }
}
