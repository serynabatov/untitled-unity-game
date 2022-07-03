public delegate void ButtonTextHandler(bool flag);

class UIDelegateHandler
{
    public event ButtonTextHandler buttonTextHandler;

    public void callResetRebinding(bool reset)
    {
        if (buttonTextHandler != null)
        {
            buttonTextHandler(reset);
        }
    }
}