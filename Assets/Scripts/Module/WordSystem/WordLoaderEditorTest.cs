/*
 * Author: CharSui
 * Created On: 2024.02.15
 * Description: Editor测试用
 */

using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
namespace Module.WordSystem
{
    public class WordLoaderEditorTest : WordLoader
    {
        private List<string> _wordContainer = new List<string>()
        {
            
            
        };

        public override bool Init()
        {
            count = 10;

            Debug.Log($"[{nameof(WordLoaderEditorTest)}]Init");
            return true;
        }

        public override bool Release()
        {
            Debug.Log($"[{nameof(WordLoaderEditorTest)}]Release");
            return true;
        }

        public override string GetWord(int wordIndex)
        {
            if (wordIndex < 0 || wordIndex > count)
            {
                Debug.LogError($"[{nameof(WordLoaderEditorTest)}]Get Wrong wordIndex:{wordIndex}");
                return default;
            }

            return _wordContainer[wordIndex];
        }
    }  
}
#endif

