using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContunueButtonReveal : MonoBehaviour
{
    [SerializeField]
    private GameObject _continueButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosXGameplay"))
        {
            _continueButton.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
