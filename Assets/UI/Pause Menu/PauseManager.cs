using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public static bool paused;
    [SerializeField] GameObject pauseBackground;
    private GameObject optionsMenu;

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
        optionsMenu = pauseBackground.transform.GetChild(1).gameObject;
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;
        paused = false;
        pauseBackground.SetActive(false);

    }

    /// <summary>
    /// TODO: Настроить кнопку опшионс
    /// </summary>
    public void Options()
    {
        optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// TODO: Изменить параметры запуска в будущем
    /// </summary>
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
