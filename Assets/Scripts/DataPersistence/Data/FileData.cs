using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This function is extensible so we can place it whenever we want
/// </summary>
[System.Serializable]
public class FileData
{
    public MetaData metaData;
    public PlayerData playerData;

    /// <summary>
    /// The values defined for test purpose only
    /// </summary>
    public MetaData()
    {
        this.metaData = new MetaData();
        this.playerData = new PlayerData();
    }
}