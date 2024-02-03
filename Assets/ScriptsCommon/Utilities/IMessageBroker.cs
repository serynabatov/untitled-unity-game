using System;
public interface IMessageBroker : IDisposable
{
    void Publish<T>(T message, int fade = 0, bool stoping = false, bool random = true, int clip = 0);
    void Subscribe<T>(Action<MessagePayload<T>> subscription);
    void Unsubscribe<T>(Action<MessagePayload<T>> subscription);
}