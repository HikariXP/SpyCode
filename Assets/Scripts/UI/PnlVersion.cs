using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlVersion : MonoBehaviour
{
    public Button BtnVersion;

    public GameObject InfoGo;

    private void Start()
    {
        RegisterEvent();
    }

    private void OnDestroy()
    {
        UnregisterEvent();
    }

    private void RegisterEvent()
    {
        BtnVersion.onClick.AddListener(ShowOrHideInfo);
    }

    private void UnregisterEvent()
    {
        BtnVersion.onClick.RemoveListener(ShowOrHideInfo);
    }

    private void ShowOrHideInfo()
    {
        InfoGo.SetActive(!InfoGo.activeInHierarchy);
    }
}
