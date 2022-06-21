using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject saveLoadMenu;
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
        SceneManager.LoadScene("Gameplay");
    }
    /// <summary>
    /// ! Убрать паузу в текущей игре
    /// </summary>
    public void ContinueGame()
    {
        PauseManager.GetInstance().ResumeGame();
    }

    /// <summary>
    /// !Начало игры с нового сейва
    /// </summary>
    public void NewGame()
    {
        // TODO: Запускал новую игру с нового сейв файла
        SceneManager.LoadScene("Gameplay");
    }

    /// <summary>
    /// ! Загрузить игру с выбранного сейва
    /// </summary>
    public void LoadGame()
    {

    }

    /// <summary>
    /// ! Сохраняет игру
    /// </summary>
    public void SaveGame()
    {
        // TODO: Чтобы реально сохранял игру
        optionsMenu.SetActive(false);
        saveLoadMenu.SetActive(saveLoadMenu.activeSelf == true ? false : true);
    }

    /// <summary>
    /// !Включает/выключает плашку options
    /// </summary>
    public void Options()
    {
        saveLoadMenu.SetActive(false);
        optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitGame()
    {
        // TODO: Изменить параметры запуска в будущем
        Application.Quit();
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// </summary>
    public void ExitToMainMenu()
    {
        // TODO: Изменить параметры запуска в будущем
        SceneManager.LoadScene("MainMenu");
    }
}
