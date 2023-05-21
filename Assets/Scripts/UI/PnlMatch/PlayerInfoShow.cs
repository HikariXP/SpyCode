using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoShow : MonoBehaviour
{
    public TextMeshProUGUI txtPlayerName;
    public TextMeshProUGUI txtPlayerLabel;

    public void SetInfo(string name,string label)
    {
        txtPlayerName.text = name;
        txtPlayerLabel.text = label;
    }
}
