using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public static bool paused;
    [SerializeField] GameObject pauseMenu;

    public PauseUIController pauseUI;
    private static PauseManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Pause Manager in the scene.");
        }
        instance = this;
    }

    public static PauseManager GetInstance()
    {
        return instance;
    } 

    private void Start()
    {
        pauseUI = pauseMenu.GetComponent<PauseUIController>();
        ResumeGame();
    }

    private void Update()
    {
        if (InputManager.GetInstance().GetEscPressed())
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;
        paused = true;
        pauseUI.pauseBackground.style.display = DisplayStyle.Flex;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;
        pauseUI.pauseBackground.style.display = DisplayStyle.None;
        paused = false;
    }
}
