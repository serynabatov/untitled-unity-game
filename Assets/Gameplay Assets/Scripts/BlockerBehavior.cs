using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBehavior : MonoBehaviour
{
    public event Action OnBlockerDisable;

    private DialogueVariables _variables;

    [SerializeField]
    private int _blockerIndex;

    [SerializeField]
    private TextAsset loadGlobalsJSON;

    private void Awake()
    {
        _variables = new DialogueVariables(loadGlobalsJSON);
    }

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
                blockStatus = PlayerPrefs.GetInt("mainVarCage", 0);
                CheckBlockerStatus(blockStatus);
                break;
        }
    }

    private void OpenPath()
    {
        if (_blockerIndex == 1)
        {
            if (PlayerPrefs.GetInt("mainVarCage") == 1)
            {
                Destroy(gameObject);
            }
        }
        if (_blockerIndex == 0)
        {
            if (PlayerPrefs.GetInt(GameManager.LastWaterSceneStatus) == 1)
            {
                OnBlockerDisable?.Invoke();
                Destroy(gameObject);
            }
        }
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
