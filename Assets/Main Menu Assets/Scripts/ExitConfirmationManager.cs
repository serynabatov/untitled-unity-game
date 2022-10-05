using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitConfirmationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject exitConfirmationWindow;

    private static ExitConfirmationManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Exit Confirmation Manager in the scene.");
        }
        instance = this;
    }

    public bool ActiveSelf()
    {
        return exitConfirmationWindow.activeSelf;
    }


    public static ExitConfirmationManager GetInstance()
    {
        return Instance;
    }

    public void SetActive(bool active)
    {
        exitConfirmationWindow.SetActive(active);
    }
}