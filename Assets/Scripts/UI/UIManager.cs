using NetworkControl.GamePlayNetwork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分场景食用。UIManager现在其实只需要有
/// </summary>
[Obsolete("原流程单场景改为分在线离线场景，在线场景中获取Server类会更加方便")]
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<PanelBase> panels = new List<PanelBase>();

    public int currentPanelIndex = 0;

    public GPNServer GPN_Server;

    public PlayerUnit localPlayerUnit { get; set; }

    private void Awake()
    {
        instance = this;
    }

    [Obsolete("原流程单场景改为分在线离线场景，在线场景中获取Server类会更加方便")]
    private void Start()
    {
        //InitAllPanelBase();

        //ResetPanels();
    }

    public void ChangePanel(int panelIndex)
    {
        currentPanelIndex = panelIndex;

        foreach (var panel in panels)
        {
            if (panel.panelIndex != currentPanelIndex)
                panel.gameObject.SetActive(false);
            else
                panel.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// 重置所有状态，通常是掉线或者刚启动的时候用
    /// </summary>
    [Obsolete("原流程单场景改为分在线离线场景，在线场景中获取Server类会更加方便")]
    public void ResetPanels()
    {
        //foreach (var panel in panels)
        //{ 
        //    panel.Reset();

        //    if(panel.panelIndex!=0)
        //    panel.gameObject.SetActive(false);
        //    else
        //    panel.gameObject.SetActive(true);
        //}

        currentPanelIndex = 0;
    }

    [Obsolete("原流程单场景改为分在线离线场景，在线场景中获取Server类会更加方便")]
    public void InitAllPanelBase()
    {
        //foreach (var panel in panels)
        //{
        //    var temp = panel.GetComponent<PanelBase>();

        //    if (temp != null)
        //    {
        //        temp.Init();
        //        Debug.Log("[UIManager]:Init " + panel.name);
        //    }
        //    else
        //    {
        //        Debug.LogError("[UIManager]:Init " + panel.name+" Fail,no Component Get");
        //    }
        //}
    }
}


