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

    public string dialogVariables;

    /// <summary>
    /// The values defined for test purpose only
    /// </summary>
    public PlayerData()
    {
        this.dialogVariables = null;
        this.playerPosition = new Vector3(-100, 18, 0);
    }

    /*
     * How to work with the keys
     * inside the key unity object do
     * 
     * public class Key : MonoBehavior {
     *     [SerializeField] private string id;
     * 
     *     [ContextMenu("Generate guid for id")]
     *     private void GenerateGuid()
     *     {
     *         id = System.Guid.NewGuid().ToString();    
     *     }    
     * }
     * 
     * Thanks to contextmenu we can just write click on the script in unity menu and generate it!
     * 
     * The storing should be done on the Key's side
    */

}