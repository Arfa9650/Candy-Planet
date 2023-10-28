using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class EventManager
{
    #region Fields

    static Dictionary<EventNames, List<IntEventInvoker>> invokers = new Dictionary<EventNames, List<IntEventInvoker>>();
    static Dictionary<EventNames, List<UnityAction<Vector2, int>>> listeners = new Dictionary<EventNames, List<UnityAction<Vector2, int>>>();

    #endregion

    #region Methods

    public static void Initialize()
    {
        foreach (EventNames name in Enum.GetValues(typeof(EventNames)))
        {
            if (!invokers.ContainsKey(name))
            {
                invokers.Add(name, new List<IntEventInvoker>());
                listeners.Add(name, new List<UnityAction<Vector2, int>>());
            }
            else
            {
                invokers[name].Clear();
                listeners[name].Clear();
            }
        }
    }

    public static void AddInvoker(EventNames eventNames, IntEventInvoker invoker)
    {
        foreach (UnityAction<Vector2, int> listener in listeners[eventNames])
        {
            invoker.AddListener(eventNames, listener);
        }
        invokers[eventNames].Add(invoker);
    }

    public static void AddListener(EventNames eventNames, UnityAction<Vector2, int> listener)
    {
        foreach (IntEventInvoker invoker in invokers[eventNames])
        {
            invoker.AddListener(eventNames, listener);
        }
        listeners[eventNames].Add(listener);
    }

    public static void RemoveInvoker(EventNames eventNames, IntEventInvoker invoker)
    {
        invokers[eventNames].Remove(invoker);
    }

    #endregion
}
