using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
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

    private Dictionary<TKey, TValue> dictionaryData = new Dictionary<TKey, TValue>();
    public Dictionary<TKey, TValue> DictionaryData
    {
        get { return dictionaryData; }
        set { dictionaryData = value; }
    }

    public void Awake()
    {
        try
        {
            for (int i = 0; i < keysList.Count; i++)
            {
                dictionaryData.Add(keysList[i], valuesList[i]);
            }
        }
        catch (Exception)
        {
            Debug.LogError("KeysList.Count is not equal to ValuesList.Count. It shouldn't happen!");
        }

    }

    public void Add(TKey key, TValue value)
    {
        dictionaryData.Add(key, value);
        keysList.Add(key);
        valuesList.Add(value);
    }

    public void Remove(TKey key)
    {
        valuesList.Remove(dictionaryData[key]);
        keysList.Remove(key);
        dictionaryData.Remove(key);
    }

    public bool ContainsKey(TKey key)
    {
        return dictionaryData.ContainsKey(key);
    }

    public bool ContainsValue(TValue value)
    {
        return dictionaryData.ContainsValue(value);
    }

    public void Clear()
    {
        dictionaryData.Clear();
        keysList.Clear();
        valuesList.Clear();
    }
}