/*
 * Copyright (c) PeroPeroGames Co., Ltd.
 * Author: CharSui
 * Created On: 2023.08.30
 * Description: 单个词语展示
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UI.GamePlayOnline;
using UnityEngine;
using UnityEngine.UI;

public class PnlWordUnit : MonoBehaviour
{
    private int _index;
    
    public Text TxtIndex;
    
    public Text TxtWordContent;

    private Button _BtnChangeWord;

    private PnlWord _pnlWord;

    private void Awake()
    {
        _BtnChangeWord = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _BtnChangeWord.onClick.AddListener(OnChangeWord);
    }

    private void OnDisable()
    {
        _BtnChangeWord.onClick.RemoveAllListeners();
    }

    private void OnChangeWord()
    {
        _pnlWord.OnPlayerClickChangeWord(_index);
    }

    /// <summary>
    /// TODO:狗屎写法没有EventManager解耦
    /// </summary>
    /// <param name="wordIndex"></param>
    /// <param name="wordContent"></param>
    /// <param name="parent"></param>
    public void Refresh(int wordIndex,string wordContent, PnlWord parent)
    {
        _pnlWord = parent;
        _index = wordIndex;
        TxtIndex.text = wordIndex.ToString();
        TxtWordContent.text = wordContent;
    }

}
