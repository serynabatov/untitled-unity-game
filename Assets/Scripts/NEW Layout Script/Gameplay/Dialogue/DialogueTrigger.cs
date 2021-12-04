using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON start")]
    [SerializeField] private TextAsset inkJSONStart;

    private bool playerInRange;
    private string varName;
    private bool varState = false;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSONStart, this.gameObject, this.ObservedVariablesList());
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    //TODO: Удалить метод
    private List<string> ObservedVariablesList()
    {
        //В этом методе будут храниться названия переменных за которыми мы следим.
        //Хранятся они в List
        List<string> variablesList = new List<string>();
     /*   variablesList.Add("lemasGay");
        variablesList.Add("egorGay");
        variablesList.Add("muzhik");
     */
        return variablesList;
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
