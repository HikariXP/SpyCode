using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlWordCardButton : MonoBehaviour
{
    private Button m_CurrentButton;

    private RectTransform m_CurrentRect;

    /// <summary>
    /// ��UI�������ṩ����ʿ�ϵͳѯ�ʵ���
    /// </summary>
    private int m_WordIndex;

    private Animator m_Animator;

    /// <summary>
    /// �Ƿ��Ѿ���ת��
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
    /// �������ת��Ƭ�������л���ʾ
    /// </summary>
    private void OnClickButton()
    {
    }
}
