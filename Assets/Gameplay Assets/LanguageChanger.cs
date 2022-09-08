using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { OnValueChange(dropdown); });
    }

    void OnValueChange(TMPro.TMP_Dropdown change)
    {
        if (change.value == 0)
        {
            PlayerPrefs.SetString("GameLanguage", "RU");
            Debug.Log("Russian"); ;
        }
        if (change.value == 1)
        {
            PlayerPrefs.SetString("GameLanguage", "EN");
            Debug.Log("English");
        }
    }
}
