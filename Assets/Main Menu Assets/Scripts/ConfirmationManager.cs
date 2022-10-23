using UnityEngine;
using UnityEngine.UI;

public class ConfirmationManager
{
    private static ConfirmationManager instance;

    public bool ActiveSelf(GameObject confirmationWindow)
    {
        return confirmationWindow.activeSelf;
    }

    public static ConfirmationManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ConfirmationManager();
        }
        return instance;
    }

    public Button GetYesButton(GameObject confirmationWindow)
    {
        return confirmationWindow.GetComponentsInChildren<Button>()[0];
    }

    public void SetActive(GameObject confirmationWindow, bool active)
    {
        confirmationWindow.SetActive(active);
    }

    public void ExecuteNoButton(GameObject confirmationWindow)
    {
        confirmationWindow.SetActive(false);
    }

    public void ChangeTextButton(string text)
    {
        // TODO: change button text here
    }
}
