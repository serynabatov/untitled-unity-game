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
            if (PlayerPrefs.HasKey("Custom resolution"))
            {
                LoadCustomResolution();
            }          
            SetResolution(PlayerPrefs.GetInt("defaultIndexResolution"));
        }
        else
        {
            AddCustomResolution();
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        //resolutionDropdown.options.ForEach(option => Debug.LogError(option.text));


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

    private void AddCustomResolution()
    {
        List<string> customResolution = new List<string>();

        bool isOriginal = true;
        int index = 0;

        customResolution.Add(Screen.currentResolution.width.ToString() + 'x' + Screen.currentResolution.height.ToString());

        foreach (TMP_Dropdown.OptionData option in resolutionDropdown.options)
        {
            if (customResolution[0] == option.text)
            {
                isOriginal = false;
                resolutionDropdown.value = index;

                PlayerPrefs.SetInt("defaultIndexResolution", index);
            }
            index++;
        }

        if (isOriginal)
        {
            resolutionDropdown.AddOptions(customResolution);
            resolutionDropdown.value = index;

            PlayerPrefs.SetInt("defaultIndexResolution", index);
            PlayerPrefs.SetString("Custom resolution", customResolution[0]);
        }
    }

    private void LoadCustomResolution()
    {
        List<string> customResolution = new List<string>();

        customResolution.Add(PlayerPrefs.GetString("Custom resolution"));

        resolutionDropdown.AddOptions(customResolution);
    }
}
