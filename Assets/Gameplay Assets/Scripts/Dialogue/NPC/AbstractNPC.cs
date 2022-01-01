using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public abstract class AbstractNPC : MonoBehaviour
{
    public abstract Story currentStory { get; set; }

    private string nameNPC { get; set; }
    private void InitializeComponents()
    {
       
    }

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
}