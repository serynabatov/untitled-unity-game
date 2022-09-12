using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CustomMap<TKey, TValue>
{

    [SerializeField]
    private List<TKey> keysList = new List<TKey>();
    public List<TKey> KeysList
    {
        get { return keysList; }
        set { keysList = value; }
    }

    [SerializeField]
    private List<TValue> valuesList = new List<TValue>();
    public List<TValue> ValuesList
    {
        get { return valuesList; }
        set { valuesList = value; }
    }

    public bool IsEmpty()
    {
        return keysList.Count == 0;
    }
}