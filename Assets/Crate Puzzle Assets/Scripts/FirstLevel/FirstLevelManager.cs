using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelManager : MonoBehaviour
{
    [Header("Gates")]
    public GameObject fireGate;
    public GameObject iceGate;
    public GameObject topGate;

    private bool fireGateActiv;
    private bool iceGateActiv;

    private bool fireTrigger;
    private bool iceTrigger;

    private static FirstLevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one First Level Manager in the scene");
        }
        instance = this;
    }

    public static FirstLevelManager GetInstance()
    {
        return instance;
    }

    public void BaseBox(bool triggerStat, GameObject triggerGO)
    {
        //Debug.Log("triggreGO = " + triggerGO.name);
        switch (triggerGO.name)
        {
            case "FinalGateTrigger":
                Debug.Log("Поздравляю первый урвоень пройден");
                topGate.SetActive(!triggerStat);
                break;
            case "BaseBoxTriggerIce":
                iceTrigger = triggerStat;
                this.CheckGates(iceGateActiv, iceGate, iceTrigger);
                break;
            case "BaseBoxTriggerFire":
                fireTrigger = triggerStat;
                this.CheckGates(fireGateActiv, fireGate, fireTrigger);
                break;
        }
    }

    public void IceBox(bool triggerStat, GameObject triggerGO)
    {
        if (triggerGO.name == "IceTrigger")
        {
            iceGateActiv = triggerStat;
            this.CheckGates(iceGateActiv, iceGate, iceTrigger);
        }
    }

    public void FireBox(bool triggerStat, GameObject triggerGO)
    {
        if (triggerGO.name == "FireTrigger")
        {
            fireGateActiv = triggerStat;
            this.CheckGates(fireGateActiv, fireGate, fireTrigger);
        }
    }

    private void CheckGates(bool gateActiv, GameObject gate, bool gateTrigger)
    {
        //Debug.Log(string.Format("active Gate = {0} , gate name = {1}", gateActiv, gate.name));
        if (gateActiv && gateTrigger)
        {
            gate.SetActive(false);
        }
        else
        {
            gate.SetActive(true);
        }
    }
}
