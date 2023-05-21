using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlWordCardButton : MonoBehaviour
{
    private Button m_CurrentButton;

    private RectTransform m_CurrentRect;

    /// <summary>
    /// 由UI创建器提供，向词库系统询问单词
    /// </summary>
    private int m_WordIndex;

    private Animator m_Animator;

    /// <summary>
    /// 是否已经翻转了
    /// </summary>
    public bool isTurn = false;

    private Vector3 trunUp = Vector3.zero;
    private Vector3 trunDown = new Vector3(0,0,180);

    private void Awake()
    {
        m_CurrentButton = GetComponent<Button>();
        m_CurrentRect = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_CurrentButton.onClick.AddListener(OnClickButton);
    }

    /// <summary>
    /// 点击后旋转卡片，并且切换显示
    /// </summary>
    private void OnClickButton()
    {
    }
}
