using System;

public class MessagePayload<T>
{
    public bool stoping;
    public int fade;
    public T payload;

    public MessagePayload(T payload,int fade, bool stoping)
    {
        this.fade = fade;
        this.payload = payload;
        this.stoping = stoping;
    }
}
