using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsTriggers : MonoBehaviour
{
    private void Start()
    {
        Boulder.OnBoulderEnd += CleanTrigger;
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
