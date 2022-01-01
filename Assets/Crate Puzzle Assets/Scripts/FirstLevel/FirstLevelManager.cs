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

    private int baseBoxCount = 0;

    private static FirstLevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
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
        if (triggerGO.name == "FinalGateTrigger")
        {
            Debug.Log("Поздравляю первый урвоень пройден");
            topGate.SetActive(!triggerStat);
        }
        if (triggerGO.name == "BaseBoxTrigger")
        {
            if (triggerStat)
            {
                baseBoxCount++;
                Debug.Log("BaseboxCount = " + baseBoxCount);
            }
            else
            {
                baseBoxCount--;
                Debug.Log("BaseboxCount = " + baseBoxCount);
            }
        }
        if (baseBoxCount >= 2 && fireGateActiv && iceGateActiv)
        {
            fireGate.SetActive(false);
            iceGate.SetActive(false);
        }
        else
        {
            fireGate.SetActive(true);
            iceGate.SetActive(true);
        }

    }

    public void IceBox(bool triggerStat,GameObject triggerGO)
    {
        if (triggerGO.name == "IceTrigger")
        {
            iceGateActiv = triggerStat;
        }
    }
    public void FireBox(bool triggerStat, GameObject triggerGO)
    {
        if (triggerGO.name == "FireTrigger")
        {
            fireGateActiv = triggerStat;
        }
    }

}
