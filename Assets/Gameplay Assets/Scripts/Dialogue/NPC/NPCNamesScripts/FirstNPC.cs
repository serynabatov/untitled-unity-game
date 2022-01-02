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
        string jsonLoad = SaveSystemManager.Load();
        if (jsonLoad != null)
        {
            npcOneModel = JsonUtility.FromJson<NPCOneModel>(jsonLoad);
        }

        //������ �� ���������� ������ ���������� �� ������
       // this.currentStory.ObserveVariable("isRunning", this.NPCDialogueFunction);
    }

    public override void NPCDialogueFunction(string variableName, object variableState)
    {
        // ��������� ����� ObserveVariable �� gameObject ������� ���������� ������
        //this.dialogueObject.SendMessage("ObserveVariable", new object[] { variableName, variableState });

        Debug.Log(string.Format("{0} = {1}", variableName, variableState));
        NPCOneModel nPCOneModel = new NPCOneModel { isRunning = (bool)variableState };
        string json = JsonUtility.ToJson(nPCOneModel);
        SaveSystemManager.Save(json);
    }

    public override List<string> ObservedVariablesList()
    {
        //� ���� ������ ����� ��������� �������� ���������� �� �������� �� ������.
        //�������� ��� � List
        List<string> variablesList = new List<string>();
        /*   variablesList.Add("lemasGay");
           variablesList.Add("egorGay");
           variablesList.Add("muzhik");
        */
        return variablesList;
    }
}