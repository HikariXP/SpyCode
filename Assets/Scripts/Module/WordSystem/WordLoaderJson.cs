using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;

namespace Module.WordSystem
{
    
    /// <summary>
    /// Json读取实现
    /// </summary>
    public class WordLoaderJson : WordLoader
    {
        private WordLibrary _wordLibrary;

        [Obsolete("似乎没用")]
        public override bool Init()
        {
            return true;
        }

        public override bool Release()
        {
            return true;
        }

        public override bool Load(string context)
        {
            if (string.IsNullOrEmpty(context))
            {
                Debug.LogError($"[{nameof(WordLoaderJson)}]Load with null context");
                return false;
            }

            try
            {
                var data = JsonConvert.DeserializeObject<WordLibrary>(context);
                _wordLibrary = data;
                count = _wordLibrary.wordDatas.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
            return true;
        }

        public override bool TryGetWord(int wordIndex, out WordData word)
        {
            if (wordIndex < 0 || wordIndex > count - 1)
            {
                word = default;
                return false;
            }

            word = _wordLibrary.wordDatas[wordIndex];
            return true;
        }
    }

}

