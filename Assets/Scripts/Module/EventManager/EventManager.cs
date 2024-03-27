using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager _instance
    {
        get
        {
            if (_instance != null)return _instance;

            _instance = new EventManager();
            return _instance;
        }
        set
        {
            if(_instance!=null)return;
            _instance = value;
        }
    }

    public static EventManager instance => _instance;

    private Dictionary<uint,NoArgEvent> _NoArgEvents = new Dictionary<uint,NoArgEvent>();
    
    // 这种无法满足需求
    // private Dictionary<uint,ArgEvent<int>> test = new Dictionary<uint,ArgEvent<int>>();
    // 多人协助的话，需要注意对Object做约束。
    private Dictionary<uint,object> _ArgEvents = new Dictionary<uint,object>();


    public NoArgEvent TryGetNoArgEvent(uint eventId)
    {
        // 如果获取到了事件
        if (_NoArgEvents.TryGetValue(eventId, out var noArgEvent)) return noArgEvent;
        
        //如果获取不了就创建
        noArgEvent = new NoArgEvent();
        _NoArgEvents.Add(eventId, noArgEvent);
        return noArgEvent;
    }

    public ArgEvent<T> TryGetArgEvent<T>(uint id)
    {
        if (_ArgEvents.TryGetValue(id, out var eventObject))
        {
            if (eventObject is ArgEvent<T> castedEvent)
            {
                return castedEvent;
            }
            Debug.LogError($"[{nameof(EventManager)}]InvalidCastException, wrong Type");
            return null;
        }
        
        var newEventTrigger = new ArgEvent<T>();
        _ArgEvents.Add(id, newEventTrigger);
        return newEventTrigger;
    }
}
