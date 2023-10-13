using System;
using System.Collections;
using System.Collections.Generic;
using NetworkControl.GamePlayNetwork;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlayOnline
{
    public class PnlWord : MonoBehaviour
    {
        public GameObject wordDisplayUnitPrefab;
        public Transform wordDisplayAnchor;

        public Button BtnDecode;

        [Header("Code")]
        public Text TxtCode;
        
        public void ShowCode(int[] code)
        {
            SetDecodeButton(false);
            string displayCode = String.Empty;
            for (int i = 0; i < code.Length; i++)
            {
                displayCode += code[i] + ".";
            }

            TxtCode.text = displayCode;
        }

        public void HideCode()
        {
            SetDecodeButton(true);
            TxtCode.text = "破译密码";
        }

        private void SetDecodeButton(bool interactable)
        {
            BtnDecode.interactable = interactable;
        }

        public void RefreshWordDisplay(List<int> wordList)
        {
            ClearChild(wordDisplayAnchor);

            for (int i = 0; i < wordList.Count; i++)
            {
                var wordIndex = wordList[i];
                var word = GPNPlayWordBackup.WordBackup[wordIndex];
                var wordUnit = Instantiate(wordDisplayUnitPrefab,wordDisplayAnchor).GetComponent<PnlWordUnit>();
                wordUnit.Refresh(i,word);
            }
        }

        private void ClearChild(Transform parent)
        {
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
            }
        }

        #region EditorTest

        [Button]
        public void RefreshWordIndex(List<int> wordIndexs)
        {
            RefreshWordDisplay(wordIndexs);
        }

        #endregion
    }
}

