using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRZJ : MonoBehaviour , IRevealable
{
    [SerializeField]
    private GameObject _bees;

    public void Reveal()
    {
        _bees.SetActive(true);
    }

    public void Conceal()
    {
        _bees.SetActive(false);
    }
}
