using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This function is extensible so we can place it whenever we want
/// </summary>
[System.Serializable]
public class MetaData
{
    public string saveName;
    public string locationName;
    public string timeStamp;

    
    /// <summary>
    /// The values defined for test purpose only
    /// </summary>
    public MetaData()
    {
        this.saveName = "";
        this.locationName = "";
        this.timeStamp = GetTimestamp(DateTime.Now);
    }

    private String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy:MM:dd:HH:mm:ss:ffff");
    }

}