using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TMP_Text visualCueText;

    [Header("Ink JSON in NPC")]
    [SerializeField] private TextAsset inkNPCEN;
    [SerializeField] private TextAsset inkNPCRU;


    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        //visualCueText = visualCue.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            CheckTextBindings();
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                MessageBrokerImpl broker = MessageBrokerImpl.Instance;
                broker.Publish<int>((int)AudioClipName.DialogueTriggerEffect);
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
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void CheckTextBindings()
    {
        if (visualCue.GetComponent<TMP_Text>().text != visualCueText.text)
        {
            visualCue.GetComponent<TMP_Text>().text = visualCueText.text;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
