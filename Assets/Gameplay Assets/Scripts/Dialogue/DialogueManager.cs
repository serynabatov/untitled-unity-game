﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour//, IDataPersistence
{
    public delegate void DialogueStatus();
    public static event DialogueStatus DialogueEnded;
    public static event DialogueStatus DialogueStarted;

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    //[SerializeField] private GameObject portrait;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;

    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string START_SCENE_TAG = "start";
    private const string EXIT_DIALOGUE_TAG = "exit";
    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        //PlayerPrefs.SetInt("Water level status", 1);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DeactivateDialoguePanel();


        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine && this.currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        DialogueStarted?.Invoke();

        SaveSystem.SavePosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        this.currentStory = new Story(inkJSON.text);

        this.dialogueIsPlaying = true;
        this.dialoguePanel.SetActive(true);
        //portrait.SetActive(true);


        dialogueVariables.StartListening(this.currentStory);

        // reset portrait, layout and speaker
        this.displayNameText.text = "???";
        this.portraitAnimator.Play("default");
        //this.layoutAnimator.Play("right");

        this.ContinueStory();
    }

    private IEnumerator ExitDialogueMode(float delayExit = 0.2f)
    {
        yield return new WaitForSeconds(delayExit);

        dialogueVariables.StopListening(this.currentStory);

        DeactivateDialoguePanel();
        dialogueText.text = "";

        DialogueEnded?.Invoke();

        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }

        //DataPersistenceManager dpManager = FindObjectsOfType<DataPersistenceManager>()[0];

        //if (dpManager != null)
        //{
        //    dpManager.SaveGame();
        //}
        //else
        //{
        //    Debug.LogError("DataPersistenceManager is not instantiated");
        //}
    }

    private void ContinueStory()
    {
        if (this.currentStory.canContinue)
        {

            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(this.currentStory.Continue()));

            //handle tags
            HandleTags(this.currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;
        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, sinish up displaying the line right away
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            //check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;

                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        // display choices, if any, for this dialogue line
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        //Loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Debug.Log(string.Format("tag key = {0} tag value = {1}", tagKey, tagValue));

            //handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case START_SCENE_TAG:
                    SceneSystem.GetInstance().LoadThisLevel(tagValue);
                    break;
                case EXIT_DIALOGUE_TAG:
                    StartCoroutine(ExitDialogueMode(float.Parse(tagValue)));
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently beign handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }
        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        if (index > 1)
        {
            continueIcon.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            this.currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // This method will get called anytime the application exists.
    // Depending on your game, you may want to save variable state in other places
    public void OnApplicationQuit()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
    }

    /*public void LoadData(PlayerData data)
    {
        this.dialogueVariables.LoadData(data);
    }

    public void SaveData(ref PlayerData data)
    {
        this.dialogueVariables.SaveData(ref data);
    } */

    public void DeactivateDialoguePanel()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        //portrait.SetActive(false);
    }
}
