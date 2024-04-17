/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 
 */

using System;
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
        if(_inputCode.Count != 3) return;
        
        var answer = _inputCode.ToArray();
        UISystem.Instance.OnPlayerConfirm(answer);
        _inputCode.Clear();
        txtDecodeDisplay.text = string.Empty;
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
        // 最大输入限制
        if (_inputCode.Count >= maxInputCount) return;
        
        // 如果不允许重复数字，则会会已输入的数字做检查
        if (!allowRepeatNumber)
        {
            foreach (var numberInputed in _inputCode)
            {
                // 有重复的内容
                if (numberInputed == number) return;
            }
        }

        _inputCode.Push(number);
        RefreshDisplay();
    }

}
