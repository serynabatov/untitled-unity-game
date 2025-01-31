using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Dropdown dropdown;
    [SerializeField]
    private bool unselect = false;

    private void Update()
    {
        if (unselect)
        {
            EventSystem.current.SetSelectedGameObject(null);
            unselect = false;
        }
    }

    void Start()
    {
        string gameLanguage;
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            gameLanguage = PlayerPrefs.GetString("GameLanguage");
            if (gameLanguage == "EN")
            {
                dropdown.value = 1;
            }
            else if (gameLanguage == "RU")
            {
                dropdown.value = 0;
            }
        }
        else
        {
            switch (Application.systemLanguage)
            {
                default:
                    {
                        PlayerPrefs.SetString("GameLanguage", "EN");
                        dropdown.value = 1;
                        break;
                    }
                case SystemLanguage.English:
                    {
                        PlayerPrefs.SetString("GameLanguage", "EN");
                        dropdown.value = 1;
                        break;
                    }
                case SystemLanguage.Russian:
                    {
                        PlayerPrefs.SetString("GameLanguage", "RU");
                        dropdown.value = 0;
                        break;
                    }
            }
        }

        dropdown.onValueChanged.AddListener(delegate
        {
            ChangeLanguageSetting(dropdown);
        });
    }

    void ChangeLanguageSetting(TMPro.TMP_Dropdown change)
    {
        unselect = true;
        if (change.value == 0)
        {
            PlayerPrefs.SetString("GameLanguage", "RU");
        }
        if (change.value == 1)
        {
            PlayerPrefs.SetString("GameLanguage", "EN");
        }
    }
}
