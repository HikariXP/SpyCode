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

        public Text TxtSuccessScore;
        public Text TxtFailScore;
        

        public Button BtnDecode;

        [Header("Code")]
        public Text TxtCode;
        
        public void ShowCode(int[] code)
        {
            if (code == null)
            {
                Debug.LogError("反馈给开发者，这里有问题[PnlWord.ShowCode获取代码为空]");
            }

            SetDecodeButton(false);
            string displayCode = String.Empty;
            for (int i = 0; i < code.Length; i++)
            {
                displayCode += code[i] + ".";
            }

            TxtCode.text = $"// {displayCode} //";
        }

        public void HideCode()
        {
            SetDecodeButton(true);
            TxtCode.text = "目标:破译密码";
        }

        public void RefreshScore(int successScore, int failScore)
        {
            TxtSuccessScore.text = successScore.ToString();
            TxtFailScore.text = failScore.ToString();
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

