using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelManager : MonoBehaviour
{
    [Header("Gates")]
    public GameObject fireGate;
    public GameObject iceGate;
    public GameObject topGate;

    [Header("Active Sprites")]
    public Sprite activeFire;
    public Sprite activeIce;
    public Sprite activeGreen;

    [Header("Deactive Sprites")]
    public Sprite deactiveFire;
    public Sprite deactiveIce;
    public Sprite deactiveGreen;

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

    public void BaseBox(bool triggerStat, GameObject triggerGO, SpriteRenderer boxSpriteGO )
    {   
        switch (triggerGO.name)
        {
            case "FinalGateTrigger":
                Debug.Log("Поздравляю первый урвоень пройден");
                topGate.SetActive(!triggerStat);
                boxSpriteGO.sprite = triggerStat ? activeGreen : deactiveGreen;
                break;
            case "BaseBoxTriggerIce":
                iceTrigger = triggerStat;
                boxSpriteGO.sprite = triggerStat ? activeGreen : deactiveGreen;
                this.CheckGates(iceGateActiv, iceGate, iceTrigger);
                break;
            case "BaseBoxTriggerFire":
                fireTrigger = triggerStat;
                boxSpriteGO.sprite = triggerStat ? activeGreen : deactiveGreen;
                this.CheckGates(fireGateActiv, fireGate, fireTrigger);
                break;
        }
    }

    public void IceBox(bool triggerStat, GameObject triggerGO, SpriteRenderer boxSpriteGO)
    {
        if (triggerGO.name == "IceTrigger")
        {
            boxSpriteGO.sprite = triggerStat ? activeIce : deactiveIce;
            iceGateActiv = triggerStat;
            this.CheckGates(iceGateActiv, iceGate, iceTrigger);
        }
    }

    public void FireBox(bool triggerStat, GameObject triggerGO, SpriteRenderer boxSpriteGO)
    {
        if (triggerGO.name == "FireTrigger")
        {
            boxSpriteGO.sprite = triggerStat ? activeFire : deactiveFire;
            fireGateActiv = triggerStat;
            this.CheckGates(fireGateActiv, fireGate, fireTrigger);
        }
    }

    private void CheckGates(bool gateActiv, GameObject gate, bool gateTrigger)
    {
        //Debug.Log(string.Format("active Gate = {0} , gate name = {1}", gateActiv, gate.name));
        /*if (gateActiv && gateTrigger)
        {
            gate.SetActive(false);
        }
        else
        {
            gate.SetActive(true);
        }*/
        gate.SetActive((gateActiv && gateTrigger) ? false : true);
    }
}
