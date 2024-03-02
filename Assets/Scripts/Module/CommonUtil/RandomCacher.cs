/*
 * Author: CharSui
 * Created On: 2024.02.25
 * Description: 存储随机数，并且在对于被换掉的数置于一定次数的保留（保留后除非移除不然不会被选中）
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCacher : MonoBehaviour
{
    private int[] _numberCache;

    /// <summary>
    /// 弃用池
    /// </summary>
    private Queue<int> _abandonCache;

    public RandomCacher()
    {
        
    }

}
