using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private GameObject loadGameMenu;

    [SerializeField]
    private GameObject exitConfirmationWindow;

    private static MenuManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Menu Manager in the scene.");
        }
        instance = this;
    }

    public static MenuManager GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// !Начало игры с последней контрольной точки
    /// </summary>
    public void ContinueGameCheckpoint()
    {
        // TODO: Загружал чтоб по последнему сайву
        if (!exitConfirmationWindow.activeSelf)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    /// <summary>
    /// ! Убрать паузу в текущей игре
    /// </summary>
    public void ContinueGame()
    {
        if (!exitConfirmationWindow.activeSelf)
        {
            PauseManager.GetInstance().ResumeGame();
        }
    }

    /// <summary>
    /// !Начало игры с нового сейва
    /// </summary>
    public void NewGame()
    {
        // TODO: Запускал новую игру с нового сейв файла
        if (!exitConfirmationWindow.activeSelf)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    /// <summary>
    /// !Загрузить игру с сейва
    /// </summary>
    public void LoadGame()
    {

        if (!exitConfirmationWindow.activeSelf)
        {
            // TODO: Запускал игру с сейв файла
            optionsMenu.SetActive(false);
            loadGameMenu.SetActive(loadGameMenu.activeSelf == true ? false : true);
        }
    }

    /// <summary>
    /// !Включает/выключает плашку options
    /// </summary>
    public void Options()
    {
        if (!exitConfirmationWindow.activeSelf)
        {
            loadGameMenu.SetActive(false);
            optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);
        }

    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitGame()
    {
        // TODO: Изменить параметры запуска в будущем
        if (!exitConfirmationWindow.activeSelf)
        {
            exitConfirmationWindow.SetActive(true);
        }
    }

    /// <summary>
    /// Окно подтверждения выхода из игры
    /// </summary>
    public void ConfirmateExitGame()
    {
        exitConfirmationWindow.SetActive(false);
        Application.Quit();
    }

    /// <summary>
    /// Окно отказа от выхода из игры
    /// </summary>
    public void DismissExitGame()
    {
        exitConfirmationWindow.SetActive(false);
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitToMainMenu()
    {
        // TODO: Изменить параметры запуска в будущем
        if (!exitConfirmationWindow.activeSelf)
        {
            exitConfirmationWindow.SetActive(true);
        }
    }

    /// <summary>
    /// Окно подтверждения выхода в главное меню
    /// </summary>
    public void ConfirmateExitToMainMenu()
    {
        exitConfirmationWindow.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Окно отказа от выхода в главное меню
    /// </summary>
    public void DismissExitToMainMenu()
    {
        exitConfirmationWindow.SetActive(false);
    }
}
