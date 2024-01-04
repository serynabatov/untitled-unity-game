using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCheck : MonoBehaviour
{
    [SerializeField]
    private Localizator localizator;
    [SerializeField]
    private string iD;

    public void ChangeLocationName()
    {
        localizator.iD = iD;
    }
}
