using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class MessageBrokerImpl : IMessageBroker
{
    private static MessageBrokerImpl _instance;

    private readonly Dictionary<Type, List<Delegate>> _subscribers;

    public static MessageBrokerImpl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MessageBrokerImpl();
            }
            return _instance;
        }
    }

    public MessageBrokerImpl()
    {
        _subscribers = new Dictionary<Type, List<Delegate>>();
    }

    public void Publish<T>(T message, bool stoping = false, bool isChangingOrRandom = false, int clip = 0)
    {
        if (message == null)
        {
            return;
        }

        if (!_subscribers.ContainsKey(typeof(T)))
        {
            return;
        }

        var delegates = _subscribers[typeof(T)];

        if (delegates == null || delegates.Count == 0)
        {
            return;
        }

        var payload = new MessagePayload<T>(message, stoping, isChangingOrRandom, clip);

        foreach (var handler in delegates.Select(item => item as Action<MessagePayload<T>>))
        {
            handler?.Invoke(payload);
        }
    }

    public void Subscribe<T>(Action<MessagePayload<T>> subscription)
    {
        var delegates = _subscribers.ContainsKey(typeof(T)) ?
        _subscribers[typeof(T)] : new List<Delegate>();
        if (!delegates.Contains(subscription))
        {
            delegates.Add(subscription);
        }
        _subscribers[typeof(T)] = delegates;
    }

    public void Unsubscribe<T>(Action<MessagePayload<T>> subscription)
    {
        if (!_subscribers.ContainsKey(typeof(T))) return;
        var delegates = _subscribers[typeof(T)];
        if (delegates.Contains(subscription))
            delegates.Remove(subscription);
        if (delegates.Count == 0)
            _subscribers.Remove(typeof(T));
    }

    public void Dispose()
    {
        _subscribers?.Clear();
    }
}

