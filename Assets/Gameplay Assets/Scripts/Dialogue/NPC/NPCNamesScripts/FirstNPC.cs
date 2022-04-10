using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Ink.Runtime;
using UnityEngine;

public class FirstNPC : AbstractNPC
{
    public override Story currentStory { get; set; }
    private NPCOneModel npcOneModel = new NPCOneModel();

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
        //В этом методе будут храниться названия переменных за которыми мы следим.
        //Хранятся они в List 
        List<string> variablesList = new List<string>();
        return variablesList;
    }

    /// <summary>
    /// Эта функция предназначена для того, чтобы НПЦ считал данные из любого поля JSON файла
    /// </summary>
    /// <param name="name"> Имя атрибута</param>
    /// <param name="npc"> У какого нпц читаем атрибут</param>
    /// <returns></returns>
    
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
            Type npcDataType = npcDataModel.GetType();
            var abstractNPCData = typeof(JsonUtility).GetMethod("FromJson").MakeGenericMethod(npcDataType)
                .Invoke(null, new object[] { jsonLoad});
            //AbstractNPCDataModel  = JsonUtility.FromJson<AbstractNPCDataModel>(jsonLoad);
            return abstractNPCData.GetType().GetProperty(name).GetValue(abstractNPCData, null);
            
        }

        return null;
    }

    public override AbstractNPCDataModel GetNPCDataModel()
    {
        return npcOneModel;
    }
}