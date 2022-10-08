using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject сonfirmationWindow;

    [SerializeField]
    private GameObject сonfirmationLoadWindow;

    private static ConfirmationManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Confirmation Manager in the scene.");
        }
        instance = this;
    }

    public bool ActiveSelf()
    {
        return сonfirmationWindow.activeSelf;
    }

    public static ConfirmationManager GetInstance()
    {
        return instance;
    }

    public Button GetYesButton()
    {
        return this.сonfirmationWindow.GetComponentsInChildren<Button>()[0];
    }

    public void SetActive(bool active)
    {
        сonfirmationWindow.SetActive(active);
    }

    public void ExecuteNoButton()
    {
        сonfirmationWindow.SetActive(false);
    }

    public void ChangeTextButton(string text)
    {
        // TODO: change button text here
    }

    public bool ActiveSelf2()
    {
        return сonfirmationLoadWindow.activeSelf;
    }

    public Button GetYesButton2()
    {
        return this.сonfirmationLoadWindow.GetComponentsInChildren<Button>()[0];
    }

    public void SetActive2(bool active)
    {
        сonfirmationLoadWindow.SetActive(active);
    }

    public void ExecuteNoButton2()
    {
        сonfirmationLoadWindow.SetActive(false);
    }

}
