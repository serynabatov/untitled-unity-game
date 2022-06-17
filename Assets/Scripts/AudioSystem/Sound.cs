using UnityEngine;


/// <summary>
/// This class keeps the information about the mp3/wav file
/// </summary>
[System.Serializable]
public class BasicSound
{
    /// <summary>
    /// The audio source.
    /// </summary>
    [HideInInspector]
    public AudioSource audioSource;

    public AudioType audioType;

    /// <summary>
    /// The mp3, wav file
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// We have enums for clipNames
    /// </summary>
    public AudioClipName clipName;

    public bool isLoop;
    public bool playOnAwake;

    [Range(0, 1)]
    public float volume = 0.5f;
}