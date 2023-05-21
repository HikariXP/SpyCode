using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnRoomControl : MonoBehaviour
{
    private Button m_CurrentButton;

    public PnlMatch m_PnlMatch;

    public enum RoomBtnKind
    { 
        cancel,
        accept
    }

    public RoomBtnKind roomBtnKind;

    private void Awake()
    {
        m_CurrentButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        m_CurrentButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        m_CurrentButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        switch (roomBtnKind)
        {
            case RoomBtnKind.accept:
                ClickAccept();break;
            case RoomBtnKind.cancel:   
                ClickCancel(); break;
        }
    }

    /// <summary>
    /// 取消连接
    /// </summary>
    private void ClickCancel()
    {
        m_PnlMatch.SetUILevel(0);
    }

    /// <summary>
    /// 准备游戏
    /// </summary>
    private void ClickAccept()
    { 
        GameNetworkManager.singleton.
    }
}
