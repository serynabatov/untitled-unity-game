using UnityEngine;
public delegate void ButtonTextHandler(bool flag);

class UIDelegateHandler
{
    public event ButtonTextHandler buttonTextHandler;

    public void callResetRebinding(bool reset)
    {
        Debug.Log("лукашенко учит итальянский");
        if (buttonTextHandler != null)
        {
            buttonTextHandler(reset);
            Debug.Log("я гей");

        }
    }
}