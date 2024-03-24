/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NetworkControl.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PnlDecode : MonoBehaviour
{
    public TMP_Text txtDecodeDisplay;

    /// <summary>
    /// 是否允许重复数字
    /// </summary>
    public bool allowRepeatNumber;

    /// <summary>
    /// 最大输入
    /// </summary>
    public int maxInputCount = 4;

    [Header("UI")]
    public Button BtnCancel;

    public Button BtnConfirm;

    private Stack<int> _inputCode;

    private StringBuilder _sb;

    public void Start()
    {
        _inputCode = new Stack<int>(maxInputCount);
        _sb = new StringBuilder();
        
        BtnCancel.onClick.AddListener(OnBackspaceBtnClick);
        BtnConfirm.onClick.AddListener(OnConfirmBtnClick);
    }

    private void OnDestroy()
    {
        BtnCancel.onClick.RemoveAllListeners();
        BtnConfirm.onClick.RemoveAllListeners();
    }

    public void Show() 
    {
        gameObject.SetActive(true);
        txtDecodeDisplay.text = String.Empty;
    }

    private void RefreshDisplay()
    {
        txtDecodeDisplay.text = string.Empty;
        var temp = _inputCode.ToArray();
        _sb.Clear();
        
        for (int i = temp.Length; i > 0; i--)
        {
            _sb.Append(temp[i-1]);
        }
        
        txtDecodeDisplay.text = _sb.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnConfirmBtnClick()
    {
        var answer = _inputCode.ToArray();
        UISystem.Instance.OnPlayerConfirm(answer);
        _inputCode.Clear();
        gameObject.SetActive(false);
    }
    
    private void OnBackspaceBtnClick()
    {
        if (_inputCode.Count <= 0) return;
        _inputCode.Pop();
        RefreshDisplay();
    }

    public void OnNumberBtnClick(int number)
    {
        _inputCode.Push(number);
        RefreshDisplay();
    }

}
