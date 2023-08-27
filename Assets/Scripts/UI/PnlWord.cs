using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 手牌页面
/// </summary>
public class PnlWord : PanelBase
{
    public Button BtnResetAllWord;
    public Button BtnConfirm;

    public Transform WordCardArchor;
    public GameObject WordCardPerfab;

    public GameObject PnlCodeDisplay;
    public TMP_Text TxtCodeDisplay;

    private const string m_CodeTips = "当前密码为:";

    private void OnEnable()
    {
        ClearChilds(WordCardArchor);
        RefreshPlayerTeamWordsDisplay();
        RegisterPlayerCallBack();
    }

    private void RegisterButtonOnClickCallBack()
    {
        BtnResetAllWord.onClick.AddListener(OnRequestChangeAllWord);
        BtnConfirm.onClick.AddListener(OnConfirm);
    }

    private void RegisterPlayerCallBack()
    {
        
    }

    private void ClearChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }



    private void RefreshPlayerTeamWordsDisplay()
    {
        //var teamIndex = UIManager.instance.localPlayerUnit.playerTeamIndex;
        //var gameNetworkManager = UIManager.instance.gameNetworkManager;

        //var list = gameNetworkManager.GetWordsByTeamIndex(teamIndex);

        //RefreshWordsDisplay(ref list);
    }

    private void RefreshWordsDisplay(ref List<string> words)
    {
        ClearChilds(WordCardArchor);

        int count = 0;
        foreach (string word in words)
        {
            var temp = Instantiate(WordCardPerfab, WordCardArchor).GetComponent<WordDisplayPerfab>();
            if (temp != null)
            {
                temp.SetWord(word, count.ToString());
                count += 1;
            }
        }
    }

    private void OnPlayerGetCode(SyncList<int> codes)
    {
        string codeDisplay = m_CodeTips;

        foreach (int i in codes)
        {
            codeDisplay += i.ToString()+".";
        }

        TxtCodeDisplay.text = codeDisplay;
        PnlCodeDisplay.SetActive(true);
    }

    private void OnRequestChangeAllWord()
    { 
        UIManager.instance.localPlayerUnit.Word_ChangeWords();
    }

    private void OnConfirm()
    {
        UIManager.instance.localPlayerUnit.Word_Confirm();
    }

    public override void Init()
    {
        RegisterButtonOnClickCallBack();
    }

    public override void Reset()
    {
        //
    }
}
