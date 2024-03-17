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
    private bool finalGateTrigger;
    private bool greenTrigger;

    private static SecondLevelManager instance;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

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
                if (triggerStat != finalGateTrigger)
                {
                    Debug.Log("Поздравляю второй уровень пройден");
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
                    this.CheckGates(iceGateActiv, iceGate, iceTrigger);
                    triggerGO.GetComponentInChildren<Animator>().Play(triggerStat ? "TriggerActive" : "TriggerDeactive");
                    boxAnimator.Play(triggerStat ? "BaseBoxActive" : "BaseBoxDeactive");
                }
                break;
            case "BaseBoxTriggerFire":
                if (triggerStat != fireTrigger)
                {
                    fireTrigger = triggerStat;
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
            _broker.Publish<int>((int)AudioClipName.IceBox);
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
            _broker.Publish<int>((int)AudioClipName.FireBox);
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
