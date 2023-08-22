using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSpawner
{
    private int m_RoundPasswordCount;
    private int m_CacheRound;

    private int m_MaxPasswordIndex;

    private Queue<int[]> cachePassword;

    public PasswordSpawner(int maxPasswordIndex = 4,int roundPasswordCount = 3,int cacheRound = 24)
    {
        m_RoundPasswordCount = roundPasswordCount;
        m_CacheRound = cacheRound;
        m_MaxPasswordIndex = maxPasswordIndex;

        cachePassword = new Queue<int[]>(cacheRound);
    }

    private int[] GetPassword()
    {
        int[] temp = new int[m_RoundPasswordCount];

        for (int i = 0; i < m_RoundPasswordCount; i++)
        {
            var passwordTemp = UnityEngine.Random.Range(0,m_MaxPasswordIndex);


        }

        return temp;
    }

    private bool IsIntArrayEqual(int[] array1, int[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }

        return true;
    }
}
