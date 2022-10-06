using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject сonfirmationWindow;

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

    public void SetActive(bool active)
    {
        сonfirmationWindow.SetActive(active);
    }
}
