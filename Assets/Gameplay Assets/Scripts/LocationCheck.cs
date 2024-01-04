using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject location;
    [SerializeField]
    private string iD;

    public void ChangeLocationName()
    {
        Localizator localizator = location.gameObject.GetComponent<Localizator>();
        Animator anim = location.gameObject.GetComponent<Animator>();
        localizator.iD = iD;
        anim.SetTrigger("RevealLocation");
    }
}
