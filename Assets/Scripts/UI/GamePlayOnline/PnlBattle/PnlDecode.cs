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
    public TMP_Text TxtDecodeDisplay;

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
        TxtDecodeDisplay.text = String.Empty;
    }

    private void RefreshDisplay()
    {
        TxtDecodeDisplay.text = string.Empty;
        var temp = _inputCode.ToArray();
        _sb.Clear();

        for (int i = temp.Length; i > 0; i--)
        {
            _sb.Append(temp[i-1]);
        }

        TxtDecodeDisplay.text = _sb.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnConfirmBtnClick()
    {
        // var answer = TxtDecodeDisplay.text;
        var answer = _inputCode.ToArray();
        UISystem.Instance.OnPlayerConfirm(answer);
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
