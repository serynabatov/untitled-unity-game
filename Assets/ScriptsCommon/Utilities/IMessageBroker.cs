using System;
public interface IMessageBroker : IDisposable
{
    void Publish<T>(T message);
    void Subscribe<T>(Action<MessagePayload<T>> subscription);
    void Unsubscribe<T>(Action<MessagePayload<T>> subscription);
}