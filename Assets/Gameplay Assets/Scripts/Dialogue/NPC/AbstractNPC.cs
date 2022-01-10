using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public abstract class AbstractNPC : MonoBehaviour
{
    public abstract Story currentStory { get; set; }

    private string nameNPC { get; set; }

    private void InitializeComponents() {}

    protected virtual void Initialize()
    {
        InitializeComponents();
    }

    public void Log(object text)
    {
        Debug.Log("[" + name + "] " + text);
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        
    }

    public abstract void EnterDialogueMode(TextAsset inkJSON);

    public abstract void NPCDialogueFunction(string variableName, object variableState);

    public abstract List<string> ObservedVariablesList();

    /// <summary>
    /// Here we're checking the data that was written to the different npcs
    /// </summary>
    /// <param name="name">name of the variable</param>
    /// <param name="npc">npc where to check</param>
    /// <returns>
    /// returning the value of an object
    /// </returns>
    public abstract object LookUpInTheState(string name, AbstractNPC npc);

    /// <summary>
    /// get model
    /// </summary>
    /// <returns>
    /// returns the model to work with the saving lookup
    /// </returns>
    public abstract AbstractNPCDataModel GetNPCDataModel();
}