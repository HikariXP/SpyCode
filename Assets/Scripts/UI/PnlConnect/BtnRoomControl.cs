//using Mirror;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class BtnRoomControl : MonoBehaviour
//{
//    private Button m_CurrentButton;

//    public PnlMatch m_PnlMatch;

//    public enum RoomBtnKind
//    { 
//        cancel,
//        accept
//    }

//    public RoomBtnKind roomBtnKind;

//    private void Awake()
//    {
//        m_CurrentButton = GetComponent<Button>();
//    }

//    private void OnEnable()
//    {
//        m_CurrentButton.onClick.AddListener(OnClick);
//    }

//    private void OnDisable()
//    {
//        m_CurrentButton.onClick.RemoveListener(OnClick);
//    }

//    private void OnClick()
//    {
//        switch (roomBtnKind)
//        {
//            case RoomBtnKind.accept:
//                ClickAccept();break;
//            case RoomBtnKind.cancel:   
//                ClickCancel(); break;
//        }
//    }

//    /// <summary>
//    /// 点击取消，告诉UI管理器让UI管理器通知GameNetworkManager做断开处理。
//    /// </summary>
//    private void ClickCancel()
//    {
//        m_PnlMatch.SetUILevel(0);
//    }

//    /// <summary>
//    /// 准备游戏，告诉UI管理器让UI管理器通知做准备处理。多次触发会来回开关准备状态。
//    /// </summary>
//    private void ClickAccept()
//    { 
        
//    }
//}
