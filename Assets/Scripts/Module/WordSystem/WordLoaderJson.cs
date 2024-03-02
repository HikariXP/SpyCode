using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Json
/// </summary>
namespace Module.WordSystem
{
    public class WordLoaderJson : WordLoader
    {
        public override bool Init()
        {
            // Addressables.LoadAssetsAsync<>
            return true;
        }

        public override bool Release()
        {
            throw new System.NotImplementedException();
        }

        public override string GetWord(int wordIndex)
        {
            throw new System.NotImplementedException();
        }
    }

}

