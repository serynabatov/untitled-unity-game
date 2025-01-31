using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject firstButton;



    private bool _isPausable;

    [SerializeField] GameObject pauseBackground;
    [SerializeField] GameObject optionsMenu;

    [SerializeField]
    private bool _hideCursorOnUnpause;

    private static PauseManager instance;

    public static event Action OnPause;
    public static event Action OnResume;

    public static bool paused;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Pause Manager in the scene.");
        }
        instance = this;
        ResumeGame();
    }

    private void Start()
    {
        DialogueManager.DialogueStarted += BlockPause;
        DialogueManager.DialogueEnded += UnBlockPause;

        UnBlockPause();
    }

    private void OnDestroy()
    {
        DialogueManager.DialogueStarted -= BlockPause;
        DialogueManager.DialogueEnded -= UnBlockPause;
    }

    public static PauseManager GetInstance()
    {
        return instance;
    }


    private void Update()
    {
        if (InputManager.GetInstance().GetEscPressed() && _isPausable)
        {
            if (paused)
            {
                if (_hideCursorOnUnpause)
                {
                    CursorScript.HideCursor();
                }

                ResumeGame();
            }
            else
            {
                CursorScript.ShowCursor();
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;
        paused = true;
        pauseBackground.SetActive(true);
        optionsMenu.SetActive(false);

        OnPause?.Invoke();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;
        paused = false;
        pauseBackground.SetActive(false);

        optionsMenu.SetActive(false);

    }

    public void BlockPause()
    {
        _isPausable = false;
    }

    public void UnBlockPause()
    {
        _isPausable = true;
    }
}
