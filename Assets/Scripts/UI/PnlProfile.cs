using System;
using Mirror;
using NetworkControl.GamePlayNetwork;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PnlProfile : MonoBehaviour
{
    public TMP_InputField inputField_Name;
    public TMP_InputField inputField_Signature;

    public Button HostButton;
    public Button ClientButton;

    private GPNServer server;

    void Start()
    {
        LoadProfile();

        RegisterButtonClickCallBack();

        server = GPNServer.instance;
    }

    private void OnDestroy()
    {
        UnregisterButtonClickCallBack();
    }

    void RegisterButtonClickCallBack()
    {
        HostButton.onClick.AddListener(PlayerDoStartHost);
        ClientButton.onClick.AddListener(PlayerDoStartClient);
    }

    void UnregisterButtonClickCallBack()
    {
        HostButton.onClick.RemoveAllListeners();
        ClientButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 
    /// </summary>
    void PlayerDoStartHost()
    {
        if (!CheckAndSaveProfile()) return;

        server.StartHost();

        Debug.Log("PnlProfile:StartHost");
    }

    /// <summary>
    /// 
    /// </summary>
    void PlayerDoStartClient()
    {
        if (!CheckAndSaveProfile()) return;

        server.StartFindServer();
    }




    private void SaveProfile()
    {
        PlayerPrefs.SetString(GPNDefine.PlayerName, inputField_Name.text);
        PlayerPrefs.SetString(GPNDefine.PlayerSignature, inputField_Signature.text);
    }

    private void LoadProfile()
    {
        var playerNameProfile = PlayerPrefs.GetString(GPNDefine.PlayerName);
        var signatureProfile = PlayerPrefs.GetString(GPNDefine.PlayerSignature);

        if(!string.IsNullOrEmpty(playerNameProfile)) inputField_Name.text = PlayerPrefs.GetString(GPNDefine.PlayerName);

        if(!string.IsNullOrEmpty(signatureProfile)) inputField_Signature.text = PlayerPrefs.GetString(GPNDefine.PlayerSignature);
    }

    private bool CheckAndSaveProfile()
    {
        if (string.IsNullOrEmpty(inputField_Name.text))
        {
            Debug.LogError("PlayerName is Empty");
            return false;
        }

        if (string.IsNullOrEmpty(inputField_Signature.text))
        {
            Debug.LogError("PlayerSignature is Empty");
            return false;
        }

        SaveProfile();

        return true;
    }
}
