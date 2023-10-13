/*
 * Author: CharSui
 * Created On: 2023.10.01
 * Description: 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using NetworkControl.UI;
using UnityEngine;
using UnityEngine.UI;

public class PnlDecode : MonoBehaviour
{
    public InputField IFPlayerDecode;

    public void Show()
    {
        gameObject.SetActive(true);
        IFPlayerDecode.text = String.Empty;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPlayerConfirm()
    {
        var answer = IFPlayerDecode.text;
        
        UISystem.Instance.OnPlayerConfirm();
    }

}
