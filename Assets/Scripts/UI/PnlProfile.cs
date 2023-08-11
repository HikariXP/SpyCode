using NetworkControl.GamePlayNetwork;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PnlProfile : PanelBase
{
    public TMP_InputField inputField_Name;
    public TMP_InputField inputField_Signature;

    public Button HostButton;
    public Button ClientButton;

    [SerializeField]
    private GPNServer server;

    void Start()
    {
        //加载曾经保存的个签和姓名
        LoadProfile();

        RegisterButtonClickCallBack();
    }

    void RegisterButtonClickCallBack()
    {
        HostButton.onClick.AddListener(PlayerDoStartHost);
        ClientButton.onClick.AddListener(PlayerDoStartClient);
    }

    /// <summary>
    /// 客户端发起Host，开始广播服务器
    /// </summary>
    void PlayerDoStartHost()
    {
        if (!CheckAndSaveProfile()) return;

        server.StartHost();

        Debug.Log("PnlProfile:StartHost");
    }

    /// <summary>
    /// 客户端发起Client,开始寻找服务器
    /// </summary>
    void PlayerDoStartClient()
    {
        if (!CheckAndSaveProfile()) return;

        Debug.Log("PnlProfile:StartFindServer");
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

    private void OnPlayerConnectedToServer()
    {
        UIManager.instance.ChangePanel(1);
    }

    public override void Reset()
    {
        Debug.Log("Reset");
    }

    public override void Init()
    {
        server = UIManager.instance.GPN_Server;
    }
}
