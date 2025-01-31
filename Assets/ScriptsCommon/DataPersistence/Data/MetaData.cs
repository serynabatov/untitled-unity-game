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
        this.timeStamp = Utilities.GetTimestamp(DateTime.Now);
    }

    public MetaData(string locationName)
    {
        this.locationName = locationName;
        this.timeStamp = Utilities.GetTimestamp(DateTime.Now);
    }

    public void ChangeTimestamp()
    {
        this.timeStamp = Utilities.GetTimestamp(DateTime.Now);
    }
}