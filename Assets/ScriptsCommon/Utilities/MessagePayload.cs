using System;

public class MessagePayload<T>
{
    public int fade;
    public T payload;

    public MessagePayload(T payload,int fade)
    {
        this.fade = fade;
        this.payload = payload;
    }
}
