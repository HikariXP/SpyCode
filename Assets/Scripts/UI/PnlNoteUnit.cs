using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PnlNoteUnit : MonoBehaviour
{
    public int noteIndex;

    public TMP_InputField noteInputField;

    public NoteUnitInfo GetNoteUnitInfo()
    { 
        var unitInfo = new NoteUnitInfo();
        unitInfo.noteIndex = noteIndex;
        unitInfo.noteContent = noteInputField.text;
        return unitInfo;
    }
}

public struct NoteUnitInfo
{
    public int noteIndex;

    public string noteContent;
}
