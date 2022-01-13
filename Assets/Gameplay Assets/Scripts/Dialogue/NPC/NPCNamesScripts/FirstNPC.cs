using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class FirstNPC : AbstractNPC
{
    public override Story currentStory { get; set; }
    private NPCOneModel npcOneModel;

    public override void EnterDialogueMode(TextAsset inkJSON)
    {

        this.currentStory = new Story(inkJSON.text);

        // Access the global variable and change its value
        string jsonLoad = SaveSystemManager.Load(npcOneModel.dataFile);
        if (jsonLoad != null)
        {
            npcOneModel = JsonUtility.FromJson<NPCOneModel>(jsonLoad);
        }
    }

    public override void NPCDialogueFunction(string variableName, object variableState)
    {
        NPCOneModel nPCOneModel = new NPCOneModel { isRunning = (bool)variableState };
        string json = JsonUtility.ToJson(nPCOneModel);
        SaveSystemManager.Save(json, npcOneModel.dataFile);
    }

    public override List<string> ObservedVariablesList()
    {
        //¬ этом методе будут хранитьс€ названи€ переменных за которыми мы следим.
        //’ран€тс€ они в List
        List<string> variablesList = new List<string>();
        return variablesList;
    }

    public override object LookUpInTheState(string name, AbstractNPC npc)
    {
        var npcDataModel = npc.GetNPCDataModel();

        if (npcDataModel.GetType().GetProperty(name) == null)
        {
            Debug.Log("This model doesn't know anything about this field " + name);
            return null;
        }

        string jsonLoad = SaveSystemManager.Load(npcDataModel.dataFile);
        if (jsonLoad != null)
        {
            AbstractNPCDataModel abstractNPCData = JsonUtility.FromJson<AbstractNPCDataModel>(jsonLoad);
            return abstractNPCData.GetType().GetProperty(name).GetValue(abstractNPCData, null);
        }

        return null;
    }

    public override AbstractNPCDataModel GetNPCDataModel()
    {
        return npcOneModel;
    }
}