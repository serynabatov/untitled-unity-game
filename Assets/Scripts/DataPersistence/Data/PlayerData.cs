using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This function is extensible so we can place it whenever we want
/// </summary>
[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;


    /// <summary>
    /// The values defined for test purpose only
    /// </summary>
    public PlayerData()
    {
        this.playerPosition = new Vector3(-100, 18, 0);
    }

    // TODO: discuss the dictionary for keys & so on


}