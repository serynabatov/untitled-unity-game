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
    private Button resumeButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button loadFirstButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button optionsFirstButton;
    [SerializeField]
    private GameObject сonfirmationWindow;

    private static MenuManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Menu Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        ConfirmateExitToMainMenu();
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
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    /// <summary>
    /// ! Убрать паузу в текущей игре
    /// </summary>
    public void ContinueGame()
    {
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
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
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            PlayerPrefs.DeleteKey("PlayerPosXGameplay");
            PlayerPrefs.DeleteKey("PlayerPosYGameplay");
            SceneManager.LoadScene("Gameplay");
        }
    }

    /// <summary>
    /// !Загрузить игру с сейва
    /// </summary>
    public void LoadGame()
    {

        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            // TODO: Запускал игру с сейв файла
            optionsMenu.SetActive(false);
            loadGameMenu.SetActive(loadGameMenu.activeSelf == true ? false : true);

            Navigation navigation = loadButton.navigation;
            navigation.selectOnRight = loadFirstButton;
            loadButton.navigation = navigation;

            navigation = resumeButton.navigation;
            navigation.selectOnRight = loadFirstButton;
            resumeButton.navigation = navigation;

            navigation = optionsButton.navigation;
            navigation.selectOnRight = loadFirstButton;
            optionsButton.navigation = navigation;

            navigation = exitButton.navigation;
            navigation.selectOnRight = loadFirstButton;
            exitButton.navigation = navigation;
        }
    }

    /// <summary>
    /// !Включает/выключает плашку options
    /// </summary>
    public void Options()
    {
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            loadGameMenu.SetActive(false);
            optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);

            Navigation navigation = optionsButton.navigation;
            navigation.selectOnRight = optionsFirstButton;
            optionsButton.navigation = navigation;

            navigation = loadButton.navigation;
            navigation.selectOnRight = optionsFirstButton;
            loadButton.navigation = navigation;

            navigation = resumeButton.navigation;
            navigation.selectOnRight = optionsFirstButton;
            resumeButton.navigation = navigation;

            navigation = exitButton.navigation;
            navigation.selectOnRight = optionsFirstButton;
            exitButton.navigation = navigation;
        }

    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitGame()
    {
        // TODO: Изменить параметры запуска в будущем
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            ConfirmationManager.GetInstance().SetActive(сonfirmationWindow, true);
        }
    }

    /// <summary>
    /// Окно подтверждения выхода из игры
    /// </summary>
    public void ConfirmateExitGame()
    {
        ConfirmationManager.GetInstance().SetActive(сonfirmationWindow, false);
        Application.Quit();
    }

    /// <summary>
    /// Окно отказа от выхода из игры
    /// </summary>
    public void DismissExitGame()
    {
        ConfirmationManager.GetInstance().SetActive(сonfirmationWindow, false);
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitToMainMenu()
    {
        // TODO: Изменить параметры запуска в будущем
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            ConfirmationManager.GetInstance().SetActive(сonfirmationWindow, true);
        }
    }

    /// <summary>
    /// Окно подтверждения выхода в главное меню
    /// </summary>
    private void ConfirmateExitToMainMenu()
    {
        ConfirmationManager.GetInstance().GetYesButton(сonfirmationWindow).onClick.AddListener(() => ExecuteYesButtonToMainMenu());
    }

    private void ExecuteYesButtonToMainMenu()
    {
        ConfirmationManager.GetInstance().SetActive(сonfirmationWindow, false);
        if (GameObject.FindWithTag("Player") != null)
        {
            SaveSystem.SavePosition(GameObject.FindWithTag("Player").transform.position);
        }
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Окно отказа от выхода в главное меню
    /// </summary>
    public void DismissExitToMainMenu()
    {
        ConfirmationManager.GetInstance().ExecuteNoButton(сonfirmationWindow);
    }

}


