using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An audio source for the entire game
/// </summary>
public class GameAudioSource : MonoBehaviour
{

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        // make sure we only have one audio source in the game!
        if (!AudioManager.Initialized)
        {
            // initialize audio manager and persist audio source across scenes
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // duplicate game object => destroy it!
            Destroy(gameObject);
        }
    }

}
