using System.Collections;
using System.Collections.Generic;
using NetworkControl.GamePlayNetwork;
using UnityEngine;

namespace UI.GamePlayOnline.PnlBattle
{
    public class PnlWord : MonoBehaviour
    {
        public GameObject wordDisplayUnitPrefab;
        public Transform wordDisplayAnchor;
        
        public void RefreshWordDisplay(List<int> wordList)
        {
            ClearChild(wordDisplayAnchor);

            for (int i = 0; i < wordList.Count; i++)
            {
                var wordIndex = wordList[i];
                var word = GPNPlayWordBackup.WordBackup[wordIndex];
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

