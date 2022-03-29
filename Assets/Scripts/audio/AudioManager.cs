using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:AudioManager"/> is initialized.
    /// </summary>
    /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initialize the audio manager
    /// </summary>
    /// <param name="source">Source.</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
