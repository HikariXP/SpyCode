using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WordSystem
{
    /// <summary>
    /// ��ǰ�ʿ�����
    /// </summary>
    private List<string> m_CurrentWordList;

    //����ʿ�����
    private int m_WordStartIndex;
    private int m_WordEndIndex;

    //����ʿ�����W
    private int m_WordSpecialStartIndex;
    private int m_WordSpecialEndIndex;

    private List<int> wordIndexHaveBeSelected = new List<int>();

    /// <summary>
    /// �����µĴʿ⡣�˲��������ԭ�дʿ⡣
    /// </summary>
    /// <returns></returns>
    public bool LoadWordCollection(ref List<string> standardWords)
    {
        if(standardWords==null)
        {
            UnityEngine.Debug.LogError("��Ĵ�������ݻ����ʿ��ǿյģ��������");
            return false;
        }

        if (standardWords.Count <= 3)
        {
            UnityEngine.Debug.LogError("�����������٣�����ʿ�");
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
    /// �����ʼ�ʿ�ļ���
    /// </summary>
    /// <param name="standardWords"></param>
    private void LoadStandardWords(ref List<string> standardWords)
    { 
        
    }
}
