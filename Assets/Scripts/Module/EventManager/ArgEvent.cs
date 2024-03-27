using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ArgEvent<T>
{

    private event Action<T> events;
    
    public void Notify(T eventArg)
    {
        events?.Invoke(eventArg);
    }

    public ArgEvent<T> Register(Action<T> callBack)
    {
        events += callBack;
        return this;
    }
    
    public ArgEvent<T> Unregister(Action<T> callBack)
    {
        events -= callBack;
        return this;
    }
}