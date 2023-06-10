using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager m_Instance;

    public static EventManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EventManager();
            }
            return m_Instance;
        }
    }
}
