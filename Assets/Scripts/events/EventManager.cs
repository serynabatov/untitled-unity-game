using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages connections between event listeners and event invokers
/// </summary>
public static class EventManager
{

    #region Fields

    static Dictionary<EventName, List<IntEventInvoker>> invokers = new Dictionary<EventName, List<IntEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<int>>> listeners = new Dictionary<EventName, List<UnityAction<int>>>();

    #endregion

    #region Public methods

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    public static void Initialize()
    {
        // create all empty lists for all the dictionary enries
        foreach (EventName name in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(name))
            {
                invokers.Add(name, new List<IntEventInvoker>());
                listeners.Add(name, new List<UnityAction<int>>());
            }
            else
            {
                invokers[name].Clear();

            }
        }
    }

    /// <summary>
    /// Adds the invoker for the given event name.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="invoker">Invoker.</param>
    public static void AddInvoker(EventName eventName, IntEventInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to dictionary
        foreach (UnityAction<int> listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        invokers[eventName].Add(invoker);
    }

    /// <summary>
    /// Removes the invoker for the given event name.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="invoker">Invoker.</param>
    public static void RemoveInvoker(EventName eventName, IntEventInvoker invoker)
    {
        // remove invoker from dictionary
        invokers[eventName].Remove(invoker);
    }

    #endregion
}
