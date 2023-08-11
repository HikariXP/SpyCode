using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PnlDecodeNumberInput : MonoBehaviour
{
    public TMP_Text tmpDecodeNumberDisplay;

    public int decodeMaxNumber = 3;

    private void OnEnable()
    {
        tmpDecodeNumberDisplay.text = string.Empty;
    }

    public void ClickNumber(int numberInput)
    {
        if (tmpDecodeNumberDisplay.text.Length >= decodeMaxNumber) return;

        if (numberInput < 0 || numberInput > 10)
        {
            Debug.LogWarning("Decode InputNumber is invaild");
            return;
        }


        tmpDecodeNumberDisplay.text += numberInput.ToString();
    }

    public void ClickBackSpace()
    {
        if (string.IsNullOrEmpty(tmpDecodeNumberDisplay.text)) return;

        string temp = tmpDecodeNumberDisplay.text.Substring(0, tmpDecodeNumberDisplay.text.Length - 1);

        tmpDecodeNumberDisplay.text= temp;

    }

    public void ClickConfirm()
    {
        
    }
}
