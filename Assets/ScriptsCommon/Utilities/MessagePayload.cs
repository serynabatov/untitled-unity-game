using System;

public class MessagePayload<T>
{
    public T payload;
    public bool stoping;
    public bool isChanging;
    public int clipNumber;

    public MessagePayload(T payload, bool stoping, bool isChanging, int clipNumber)
    {
        this.payload = payload;
        this.stoping = stoping;
        this.isChanging = isChanging;
        this.clipNumber = clipNumber;
    }
}
