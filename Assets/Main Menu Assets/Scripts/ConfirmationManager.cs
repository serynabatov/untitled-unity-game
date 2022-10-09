using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmationManager : MonoBehaviour
{
    private static ConfirmationManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Confirmation Manager in the scene.");
        }
        instance = this;
    }

    public bool ActiveSelf(GameObject confirmationWindow)
    {
        return сonfirmationWindow.activeSelf;
    }

    public static ConfirmationManager GetInstance()
    {
        return instance;
    }

    public Button GetYesButton(GameObject confirmationWindow)
    {
        return сonfirmationWindow.GetComponentsInChildren<Button>()[0];
    }

    public void SetActive(GameObject confirmationWindow, bool active)
    {
        сonfirmationWindow.SetActive(active);
    }

    public void ExecuteNoButton(GameObject confirmationWindow)
    {
        сonfirmationWindow.SetActive(false);
    }

    public void ChangeTextButton(string text)
    {
        // TODO: change button text here
    }
}
