using System;

public class MessagePayload<T>
{
    public T payload;

    public MessagePayload(T payload)
    {
        this.payload = payload;
    }
}
