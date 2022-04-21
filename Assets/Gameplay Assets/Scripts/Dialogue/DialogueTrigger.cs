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
                DialogueManager.GetInstance().EnterDialogueMode(inkJSONStart);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

   /* public List<string> ObservedVariablesList()
    {
        //� ���� ������ ����� ��������� �������� ���������� �� �������� �� ������.
        //�������� ��� � List 
        List<string> variablesList = new List<string>();
        variablesList.Add("mainVarCage");
        variablesList.Add("certificate");
        variablesList.Add("keyCount");
        return variablesList;
    }

    public void ChangeVariableDialogueFunction(string variableName, object variableState)
    {
        // ������� � ������� ���������� ������� �������� ��������
        Debug.Log(string.Format("Var NAME = {0}, Var STATE = {1}", variableName, variableState));

        //����� �����, ������� �� ��������� � ���� ����� �������� ���������� �� � �������� � ���������� variableName
        // � �������� � ���������� variableState
    }*/

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
