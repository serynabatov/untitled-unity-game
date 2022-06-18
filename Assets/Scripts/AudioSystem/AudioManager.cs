using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

/// <summary>
/// Audio manager.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public const string preferenceAudioMute = "preferenceAudioMute";

    [SerializeField]
    private CustomMap<AudioClipName, BasicSound> sounds;

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup soundEffectMixerGroup;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(preferenceAudioMute))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(preferenceAudioMute);
        }


        foreach (KeyValuePair<AudioClipName, BasicSound> entry in sounds.DictionaryData)
        {
            BasicSound s = entry.Value;

            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.isLoop;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.volume = s.volume;

            switch (s.audioType)
            {
                case AudioType.SoundEffect:
                    s.audioSource.outputAudioMixerGroup = soundEffectMixerGroup;
                    break;

                case AudioType.MainMusic:
                    s.audioSource.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
            {
                s.audioSource.Play();
            }
        }
    }

    private BasicSound GetSound(AudioClipName audioClipName)
    {
        BasicSound sound;
        if (!sounds.DictionaryData.TryGetValue(audioClipName, out sound))
        {
            // nothing here
            return null;
        }
        return sound;
    }

    /// <summary>
    /// Plays the specified audioClip.
    /// </summary>
    /// <param name="audioClipName">Audio clip name.</param>
    public void Play(AudioClipName audioClipName)
    {
        BasicSound s = GetSound(audioClipName);
        if (s != null)
        {
            s.audioSource.Play();
        }
    }

    /// <summary>
    /// Stop the specified audioClipName.
    /// </summary>
    /// <param name="audioClipName">Audio clip name.</param>
    public void Stop(AudioClipName audioClipName)
    {
        BasicSound s = GetSound(audioClipName);
        if (s != null)
        {
            s.audioSource.Stop();
        }
    }

    /// <summary>
    /// Mute the sounds.
    /// </summary>
    public void Mute()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

        PlayerPrefs.SetFloat(preferenceAudioMute, AudioListener.volume);
    }

    /// <summary>
    /// Updates the mixer volume.
    /// </summary>
    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectMixerGroup.audioMixer.SetFloat("SoundsEffect", Mathf.Log10(AudioOptionsManager.soundsEffectVolume) * 20);
    }
}