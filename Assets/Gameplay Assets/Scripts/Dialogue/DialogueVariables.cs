using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES";


    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        // if we have saved data, load it
        if (PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }


        //initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            // Load the current state of all of our variables to the globals story
            VariablesToStory(globalVariablesStory);
            // NOTE: eventually, you'd want to replace this with an actual save/load method
            // rather than using PlayerPrefs
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }

    public void StartListening(Story story)
    {
        // it's important that VariableToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + name + " = " + value);
        // only maintain variables that vere initialized from the globals ink file
        if (variables.ContainsKey(name))
        {
            if (variables["mainVarCage"].ToString().ToLower() == "true")
            {
                PlayerPrefs.SetInt("mainVarCage", 1);
            }

            if (variables["mainVarBossFinished"].ToString().ToLower() == "true")
            {
                PlayerPrefs.SetInt(GameManager.LastWaterSceneStatus, 1);
            }

            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }


    public void LoadData(PlayerData data)
    {
        if (!string.IsNullOrEmpty(data.dialogVariables))
        {
            globalVariablesStory.state.LoadJson(data.dialogVariables);
            variables.Clear();
            foreach (string name in globalVariablesStory.variablesState)
            {
                Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
                Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
            }
        }
    }

    public void SaveData(ref PlayerData data)
    {
        if (globalVariablesStory != null)
        {
            VariablesToStory(globalVariablesStory);
            data.dialogVariables = globalVariablesStory.state.ToJson();
        }
    }
}
