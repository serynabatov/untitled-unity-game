using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool paused;
    List<int> widths = new List<int>() { 1920, 1280, 960 };
    List<int> heights = new List<int>() { 1080, 800, 540 };
    [SerializeField] GameObject pauseBackground;
    [Header("Graphics")]
    [SerializeField] Toggle fullscreenToogle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [Header("Sounds")]
    [SerializeField] Slider musicVolumeSlider;
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
        //! Разрешение экрана дефолтное изменённое запускаем и фуллскрин или в окне
        SetResolution(PlayerPrefs.GetInt("defaultIndexResolution", 0));
        SetFullscreen(PlayerPrefs.GetInt("fullscreenStatus", 1) == 1 ? true : false);

        //* Громкость музыки настройка
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1.0f));
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);


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

    /// <summary>
    //  *Sets resolution
    /// </summary>
    /// <param name="index"></param>
    public void SetResolution(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
        PlayerPrefs.SetInt("defaultIndexResolution", index);

        resolutionDropdown.value = index;

    }
    /// <summary>
    /// *Sets fullscreen status
    /// </summary>
    /// <param name="_fullscreen"></param>
    public void SetFullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
        fullscreenToogle.isOn = _fullscreen;
        PlayerPrefs.SetInt("fullscreenStatus", _fullscreen == true ? 1 : 0);
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
