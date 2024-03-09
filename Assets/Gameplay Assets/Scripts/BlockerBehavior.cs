using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBehavior : MonoBehaviour
{
    public event Action OnBlockerDisable;

    [SerializeField]
    private int _blockerIndex;
    void Start()
    {
        DisableBlocker(_blockerIndex);
        DialogueManager.DialogueEnded += OpenPath;
    }

    private void OnDestroy()
    {
        DialogueManager.DialogueEnded -= OpenPath;
    }

    private void DisableBlocker(int index)
    {
        int blockStatus;
        switch (index)
        {
            case 0:
                blockStatus = PlayerPrefs.GetInt(GameManager.LastWaterSceneStatus, 0);
                CheckBlockerStatus(blockStatus);
                break;
            case 1:
                blockStatus = PlayerPrefs.GetInt("mainVarCrate", 0);
                CheckBlockerStatus(blockStatus);
                break;
        }
    }

    private void OpenPath()
    {
        if (_blockerIndex == 1)
        {
            PlayerPrefs.SetInt("mainVarCrate", 1);
            Destroy(gameObject);
        }
        DialogueManager.DialogueEnded -= OpenPath;
    }

    private void CheckBlockerStatus(int status)
    {
        if (status == 1)
        {
            OnBlockerDisable?.Invoke();
            Destroy(gameObject);
        }
    }
}
