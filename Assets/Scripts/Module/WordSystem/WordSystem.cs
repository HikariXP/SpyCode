using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WordSystem
{
    /// <summary>
    /// 当前词库内容
    /// </summary>
    private List<string> m_CurrentWordList;

    //常规词库索引
    private int m_WordStartIndex;
    private int m_WordEndIndex;

    //特殊词库索引
    private int m_WordSpecialStartIndex;
    private int m_WordSpecialEndIndex;

    /// <summary>
    /// 加载新的词库。此操作会清空原有词库。
    /// </summary>
    /// <returns></returns>
    public bool LoadWordCollection(ref List<string> standardWords,ref List<string> specialWords)
    {
        if(standardWords==null)
        {
            UnityEngine.Debug.LogError("妈的传入的数据基础词库是空的，检查输入");
            return false;
        }

        if (standardWords.Count <= 3)
        {
            UnityEngine.Debug.LogError("词语数量过少，请检查词库");
            return false;
        }

        if (m_CurrentWordList == null)
        {
            m_CurrentWordList = new List<string>();
        }

        m_CurrentWordList.Clear();

        LoadStandardWords(ref standardWords);

        return true;
    }

    /// <summary>
    /// 处理初始词库的加载
    /// </summary>
    /// <param name="standardWords"></param>
    private void LoadStandardWords(ref List<string> standardWords)
    { 
    
    }
}
