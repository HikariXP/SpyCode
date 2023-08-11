using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对于进入战局前，房间的链接和房间内操作的面板的激活逻辑控制
/// </summary>
public class PnlConnect : MonoBehaviour
{
    public enum ConnetPanelOrder
    { 
        modeSelect = 0,
        inRoom
    }

    /* 理论的顺序是
     * 1、模式选择
     * 2、房间面板
    */
    public List<GameObject> panels = new List<GameObject>();

    private void Start()
    {
        IndepencecyInject();
    }

    private void IndepencecyInject()
    {
        foreach (GameObject panelGameObject in panels)
        {
            var panelTemp = panelGameObject.GetComponent<PnlConnectUnit>();
            panelTemp.SetOrderRecevier(this);
            panelTemp.Init();
        }
    }

    public void ChangePanel(ConnetPanelOrder targetPanel)
    {
        foreach(GameObject panelGameObject in panels)
        { 
            panelGameObject.SetActive(false);    
        }

        panels[(int)targetPanel].SetActive(true);
    }

    public void ResetConnectPanel()
    {
        ChangePanel(ConnetPanelOrder.modeSelect);
    }
}

public interface PnlConnectUnit
{
    void SetOrderRecevier(PnlConnect pnlConnect);

    void Init();
}