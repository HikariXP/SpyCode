using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDisplayPerfab : MonoBehaviour
{
    public TextMeshProUGUI txtWordIndex;
    public TextMeshProUGUI txtWord;

    public void SetWord(string word, string wordIndex)
    {
        txtWord.text = word;
        txtWordIndex.text = wordIndex;
    }
}
