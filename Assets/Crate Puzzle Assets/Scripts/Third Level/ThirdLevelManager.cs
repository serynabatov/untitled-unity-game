using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelManager : MonoBehaviour
{
    [Header("Gates")]
    public GameObject fireGate;
    public GameObject iceGate;
    public GameObject topGate;
    public GameObject greenGate;
    public GameObject blueGate;
    public GameObject orangeGate;

    private bool fireGateActiv;
    private bool iceGateActiv;

    private bool fireTrigger;
    private bool iceTrigger;
    private bool finalGateTrigger;
    private bool greenTrigger;
    private static ThirdLevelManager instance;
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    private DialogueVariables dialogueVariables;

    private MessageBrokerImpl broker;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Third Level Manager in the scene");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }
    public static ThirdLevelManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        broker = MessageBrokerImpl.Instance;
    }

    public void BaseBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        switch (triggerGO.name)
        {
            case "FinalGateTrigger":
                if (triggerStat != finalGateTrigger)
                {
                    Debug.Log("Поздравляю третий уровень пройден");
                    finalGateTrigger = triggerStat;
                    topGate.SetActive(!triggerStat);
                    triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                    boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                }
                break;
            case "BaseBoxTriggerIce":
                if (triggerStat != iceTrigger)
                {
                    iceTrigger = triggerStat;
                    blueGate.SetActive(!triggerStat);
                    this.CheckGates(iceGateActiv, iceGate, iceTrigger);
                    triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                    boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                }
                break;
            case "BaseBoxTriggerFire":
                if (triggerStat != fireTrigger)
                {
                    fireTrigger = triggerStat;
                    orangeGate.SetActive(!triggerStat);
                    this.CheckGates(fireGateActiv, fireGate, fireTrigger);
                    triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                    boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                }
                break;
            case "BaseBoxTriggerGate":
                if (triggerStat != greenTrigger)
                {
                    greenTrigger = triggerStat;
                    greenGate.SetActive(!triggerStat);
                    triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                    boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                }
                break;
        }
    }

    public void IceBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        if (triggerGO.name == "IceTrigger" && triggerStat != iceGateActiv)
        {
            triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
            broker.Publish<int>(17);
            boxAnimator.Play(triggerStat ? "IceActive" : "IceDeactive");
            iceGateActiv = triggerStat;
            this.CheckGates(iceGateActiv, iceGate, iceTrigger);
        }
    }

    public void FireBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        if (triggerGO.name == "FireTrigger" && triggerStat != fireGateActiv)
        {
            triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
            broker.Publish<int>(18);
            boxAnimator.Play(triggerStat ? "FireActive" : "FireDeactive");
            fireGateActiv = triggerStat;
            this.CheckGates(fireGateActiv, fireGate, fireTrigger);
        }
    }

    private void CheckGates(bool gateActiv, GameObject gate, bool gateTrigger)
    {
        //Debug.Log(string.Format("active Gate = {0} , gate name = {1}", gateActiv, gate.name));
        gate.SetActive((gateActiv && gateTrigger) ? false : true);
    }
    public void CratePuzzleSolved()
    {
        PlayerPrefs.SetInt("Crate level status", 1);
        PlayerPrefs.DeleteKey("PlayerPosXCrate");
        PlayerPrefs.DeleteKey("PlayerPosYCrate");
        if (dialogueVariables.variables.ContainsKey("mainVarCrateFinished"))
        {
            dialogueVariables.variables["mainVarCrateFinished"] = Ink.Runtime.BoolValue.Create(true);
            dialogueVariables.SaveVariables();
            //dialogueVariables.variables.Add("mainVarWaterFinished", true);
        }
        if (PlayerPrefs.GetInt("Water level status") == PlayerPrefs.GetInt("Crate level status") == 1)
        {
            dialogueVariables.variables["cantalktoBoss"] = Ink.Runtime.BoolValue.Create(true);
            dialogueVariables.SaveVariables();
        }
    }
}
