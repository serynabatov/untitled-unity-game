using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDialogue : MonoBehaviour
{
    [Header("Ink JSON in NPC")]
    [SerializeField] private TextAsset inkNPCEN;
    [SerializeField] private TextAsset inkNPCRU;


    void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerPosXGameplay"))
        {
            print("first dialogue");

            switch (PlayerPrefs.GetString("GameLanguage"))
            {
                case "RU":
                    DialogueManager.GetInstance().EnterDialogueMode(inkNPCRU);
                    break;
                case "EN":
                    DialogueManager.GetInstance().EnterDialogueMode(inkNPCEN);
                    break;
                default:
                    DialogueManager.GetInstance().EnterDialogueMode(inkNPCRU);
                    break;
            }
        }
    }
}
