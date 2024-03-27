using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NoArgEvent
{
    private event Action events;
    
    public void Notify()
    {
        events?.Invoke();
    }

    public NoArgEvent Register(Action callBack)
    {
        events += callBack;
        return this;
    }
    
    public NoArgEvent Unregister(Action callBack)
    {
        events -= callBack;
        return this;
    }
}
