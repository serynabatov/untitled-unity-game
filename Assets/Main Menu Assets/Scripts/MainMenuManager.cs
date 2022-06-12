using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    List<int> widths = new List<int>() { 1920, 1280, 960 };
    List<int> heights = new List<int>() { 1080, 800, 540 };
    [Header("Graphics")]
    [SerializeField] Toggle fullscreenToogle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [Header("Sounds")]
    [SerializeField] Slider musicVolumeSlider;
    /* private static MainMenuManager instance;
     private void Awake()
     {
         if (instance != null)
         {
             Debug.LogError("Found more than one Main Menu Manager in the scene.");
         }
         instance = this;
     }

     public static MainMenuManager GetInstance()
     {
         return instance;
     } */
    void Start()
    {
        //* Разрешение экрана дефолтное изменённое запускаем и фуллскрин или в окне
        if (PlayerPrefs.HasKey("defaultIndexResolution"))
        {
            SetResolution(PlayerPrefs.GetInt("defaultIndexResolution", 0));
        }
        if (PlayerPrefs.HasKey("fullscreenStatus"))
        {
            SetFullscreen(PlayerPrefs.GetInt("fullscreenStatus", 1) == 1 ? true : false);
        }
        //* Громкость музыки настройка
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1.0f));
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void Options()
    {
        optionsMenu.SetActive(optionsMenu.activeSelf == true ? false : true);
    }

    /// <summary>
    /// *Запуск стандартной меин меню сцены
    /// TODO: Изменить параметры запуска в будущем
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
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

    /// <summary>
    /// !Sets only musics volume
    /// TODO: Добавить ещё отдельный код для слайдерный звук эффектов
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
