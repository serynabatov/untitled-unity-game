using UnityEngine;
using UnityEngine.Audio;

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

    public AudioClipName audioClipName;

    /// <summary>
    /// The mp3, wav file
    /// </summary>
    public AudioClip[] audioClip;

    public bool isLoop;
    public bool playOnAwake;

    public AudioRolloffMode AudioRolloffMode;

    public AudioMixerGroupEnum mixerGroup;
    public float minDistance;
    public float maxDistance;
    [Range(0, 1)]
    public float volume = 0.5f;
    [Range(0, 1)]
    public float spatialBlend = 0.0f;
}