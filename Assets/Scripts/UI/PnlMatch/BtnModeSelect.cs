using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnModeSelect : MonoBehaviour
{
    private Button m_CurrentButton;

    public GameMode buttonGameMode;

    public PnlMatch m_PnlMatch;

    private void Awake()
    {
        m_CurrentButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        m_CurrentButton.onClick.AddListener(OnStartGameManager);
    }

    private void OnDisable()
    {
        m_CurrentButton.onClick.RemoveListener(OnStartGameManager);
    }

    private void OnStartGameManager()
    {
        m_PnlMatch.SetUILevel(1);

        m_PnlMatch.StartGameManager(buttonGameMode);
    }
}

