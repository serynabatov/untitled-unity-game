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
    private GameObject creditsPanel;

    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button creditsButton;
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
        SceneSystem.GetInstance().LoadThisLevel(PlayerPrefs.GetString("Saved scene"));
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
    /// !Загрузить игру с сейва
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Credits()
    {
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            optionsMenu.SetActive(false);
            creditsPanel.SetActive(creditsPanel.activeSelf == true ? false : true);
        }

    }

    /// <summary>
    /// !Включает/выключает плашку options
    /// </summary>
    public void Options()
    {

        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationWindow))
        {
            if (creditsPanel != null)
            {
                creditsPanel.SetActive(false);
            }
            optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);

            Navigation navigation = optionsButton.navigation;
            navigation.selectOnRight = optionsFirstButton;
            optionsButton.navigation = navigation;

            if (creditsButton != null)
            {
                navigation = creditsButton.navigation;
                navigation.selectOnRight = optionsFirstButton;
                creditsButton.navigation = navigation;
            }

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
        PauseManager.GetInstance().ResumeGame();
        SceneSystem.GetInstance().LoadThisLevel("MainMenu");
    }

    /// <summary>
    /// Окно отказа от выхода в главное меню
    /// </summary>
    public void DismissExitToMainMenu()
    {
        ConfirmationManager.GetInstance().ExecuteNoButton(сonfirmationWindow);
    }

    public void ExitOnEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


