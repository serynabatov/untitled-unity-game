using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [Header("Graphics")]
    [SerializeField] Toggle fullscreenToogle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    private static OptionsManager instance;

    private bool unselect;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Options Manager in the scene.");
        }
        instance = this;
    }

    private void Update()
    {
        if (unselect)
        {
            EventSystem.current.SetSelectedGameObject(null);
            unselect = false;
        }
    }

    public static OptionsManager GetInstance()
    {
        return instance;
    }
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
    }

    /// <summary>
    ///  *Sets resolution
    /// </summary>
    /// <param name="index"></param>
    public void SetResolution(int index)
    {
        string[] resolution = resolutionDropdown.options[index].text.Split('x');
        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Screen.fullScreen);
        PlayerPrefs.SetInt("defaultIndexResolution", index);
        resolutionDropdown.value = index;
        unselect = true;
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
        EventSystem.current.SetSelectedGameObject(null);
    }
}
