/*
 * Author: CharSui
 * Created On: 2024.04.06
 * Description: 展示战局所选的单词
 */
using System;
using System.Collections.Generic;
using Module.WordSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GamePlayOnline
{
    public class PnlWord : MonoBehaviour,IInitAndReset
    {
        public GameObject wordDisplayUnitPrefab;
        public Transform wordDisplayAnchor;

        public Text TxtSuccessScore;
        public Text TxtFailScore;
        
        public Button BtnConfirmWordList;
        public Button BtnDecode;

        [Header("Code")]
        public Text TxtCode;

        private void Awake()
        {
            BtnConfirmWordList.onClick.AddListener(OnPlayerConfirmWordList);
        }

        private void OnDisable()
        {
            BtnConfirmWordList.onClick.RemoveAllListeners();
        }
        
        public void UI_Init()
        {
            BtnConfirmWordList.gameObject.SetActive(true);
            BtnDecode.gameObject.SetActive(false);
        }

        public void UI_Reset()
        {
            
        }

        public void ShowCode(int[] codes)
        {
            if (codes == null)
            {
                Debug.LogError("反馈给开发者，这里有问题[PnlWord.ShowCode获取代码为空]");
                return;
            }

            SetDecodeButton(false);
            string displayCode = String.Empty;
            foreach (var code in codes)
            {
                displayCode += code + ".";
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

        private void OnPlayerConfirmWordList()
        {
            BattleHelper.LocalPlayerUnit.Cmd_PlayerConfirmWordList();
        }

        public void OnTeamEndWordSelected()
        {
            BtnConfirmWordList.gameObject.SetActive(false);
            BtnDecode.gameObject.SetActive(true);
        }

        public void RefreshWordDisplay(List<WordData> wordList)
        {
            ClearChild(wordDisplayAnchor);

            for (int i = 0; i < wordList.Count; i++)
            {
                var wordIndex = i;
                var word = wordList[i];
                var wordUnit = Instantiate(wordDisplayUnitPrefab,wordDisplayAnchor).GetComponent<PnlWordUnit>();
                wordUnit.Refresh(wordIndex, word.word_localization, word.word_en, this);
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
    }
}

