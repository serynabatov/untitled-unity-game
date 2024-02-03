using System;

public class MessagePayload<T>
{
    public int fade;
    public T payload;
    public bool stoping;
    public bool isChanging;
    public int clipNumber;

    public MessagePayload(T payload, int fade, bool stoping, bool isChanging, int clipNumber)
    {
        this.fade = fade;
        this.payload = payload;
        this.stoping = stoping;
        this.isChanging = isChanging;
        this.clipNumber = clipNumber;
    }
}
