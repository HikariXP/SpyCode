/*
 * Copyright (c) PeroPeroGames Co., Ltd.
 * Author: CharSui
 * Created On: 2023.08.30
 * Description: 单个词语展示
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlWordUnit : MonoBehaviour
{
    public Text TxtIndex;
    
    public Text TxtWordContent;

    public void Refresh(int wordIndex,string wordContent)
    {
        TxtIndex.text = wordIndex.ToString();
        TxtWordContent.text = wordContent;
    }

}
