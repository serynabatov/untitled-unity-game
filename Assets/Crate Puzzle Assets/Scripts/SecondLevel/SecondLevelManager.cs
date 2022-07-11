using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelManager : MonoBehaviour
{
    [Header("Gates")]
    public GameObject fireGate;
    public GameObject iceGate;
    public GameObject topGate;
    public GameObject greenGate;

    private bool fireGateActiv;
    private bool iceGateActiv;

    private bool fireTrigger;
    private bool iceTrigger;

    private static SecondLevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Second Level Manager in the scene");
        }
        instance = this;

    }
    public static SecondLevelManager GetInstance()
    {
        return instance;
    }

    public void BaseBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        switch (triggerGO.name)
        {
            case "FinalGateTrigger":
                Debug.Log("Поздравляю второй уровень пройден");
                topGate.SetActive(!triggerStat);
                triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                break;
            case "BaseBoxTriggerIce":
                iceTrigger = triggerStat;
                this.CheckGates(iceGateActiv, iceGate, iceTrigger);
                triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                break;
            case "BaseBoxTriggerFire":
                fireTrigger = triggerStat;
                this.CheckGates(fireGateActiv, fireGate, fireTrigger);
                triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                break;
            case "BaseBoxTriggerGate":
                greenGate.SetActive(!triggerStat);
                triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                break;
        }
    }

    public void IceBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        if (triggerGO.name == "IceTrigger")
        {
            triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
            boxAnimator.Play(triggerStat ? "IceActive" : "IceDeactive");
            iceGateActiv = triggerStat;
            this.CheckGates(iceGateActiv, iceGate, iceTrigger);
        }
    }

    public void FireBox(bool triggerStat, GameObject triggerGO, Animator boxAnimator)
    {
        if (triggerGO.name == "FireTrigger")
        {
            triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
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
}
