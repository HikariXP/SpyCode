/*
 * Author: CharSui
 * Created On: 2024.03.03
 * Description: 具有缓存器的数字生成器，一个模块自己生成，用于获取不重复的数字
 * 生成的数字是连续的，如果需要排除某些数字，需要自己处理
 * TODO:后续可以拓展其他更多的随机类型
 */
using System.Collections.Generic;
using UnityEngine;

namespace Module.CommonUtil
{
    public class RandomCacher
    {
        private int _maxExcluesive;
    
        // 清空缓存的次数 = 最大值除以2
        private int clearCacheCount => _maxExcluesive / 2;
        
        private HashSet<int> _numberCache;
        
        public RandomCacher(int maxExclusive)
        {
            _maxExcluesive = maxExclusive;
            _numberCache = new HashSet<int>(_maxExcluesive);
        }
    
        public void ClearCache()
        {
            _numberCache.Clear();
        }
        
        /// <summary>
        /// 如果获取的数字
        /// </summary>
        /// <returns></returns>
        public bool GetNumber(out int resultNumber)
        {
            // 最大获取次数
            var checkCount = 0;
            while (true)
            { 
                //1、获取随机数
                var number = Random.Range(0, _maxExcluesive);
                //2、如果不重复则添加
                if (!_numberCache.Contains(number))
                {
                    _numberCache.Add(number);
                    resultNumber = number;
                    return true;
                }
    
                
                //循环十次也没有获得答案就清空缓存
                if (checkCount > clearCacheCount)
                {
                    _numberCache.Clear();
                    resultNumber = number;
                    return false;
                }
    
                checkCount += 1;
            }
        }
    }
}

