using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnlConnect : MonoBehaviour
{
    public enum ConnetPanelOrder
    { 
        modeSelect = 0,
        inRoom
    }

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