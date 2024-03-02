using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private EventManager _instance;

    public EventManager instance => _instance;

    private void Awake()
    {
        _instance = new EventManager();
    }
    
    // private delegate void _noArgEvent;
}
