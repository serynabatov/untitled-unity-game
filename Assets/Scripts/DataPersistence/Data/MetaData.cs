using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This function is extensible so we can place it whenever we want
/// </summary>
[System.Serializable]
public class MetaData
{
    public string locationName;
    public string timeStamp;


    /// <summary>
    /// The values defined for test purpose only
    /// </summary>
    public MetaData()
    {
        this.locationName = "";
        this.timeStamp = GetTimestamp(DateTime.Now);
    }

    public MetaData(string locationName)
    {
        this.locationName = locationName;
        this.timeStamp = GetTimestamp(DateTime.Now);
    }

    private string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy:MM:dd:HH:mm:ss:ffff");
    }

}