using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main for the entry game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        EventManager.Initialize();
    }

}
