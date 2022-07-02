using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject firstButton;

    
    public static bool paused;
    [SerializeField] GameObject pauseBackground;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject loadGameMenu;
    private static PauseManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Pause Manager in the scene.");
        }
        instance = this;
        ResumeGame();
    }

    public static PauseManager GetInstance()
    {
        return instance;
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
        pauseBackground.SetActive(true);
        optionsMenu.SetActive(false);
        loadGameMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;
        paused = false;
        pauseBackground.SetActive(false);

    }
}
