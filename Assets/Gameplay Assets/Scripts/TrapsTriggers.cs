using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsTriggers : MonoBehaviour
{
    private void Start()
    {
        Boulder.OnBoulderEnd += CleanTrigger;
        if (PlayerPrefs.GetInt("BoulderStatus") == 1)
        {
            CleanTrigger();
        }
    }

    private void OnDestroy()
    {
        Boulder.OnBoulderEnd -= CleanTrigger;
    }

    private void CleanTrigger()
    {
        Destroy(gameObject);
    }
}
