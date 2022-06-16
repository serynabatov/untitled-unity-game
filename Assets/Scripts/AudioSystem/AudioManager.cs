using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

/// <summary>
/// Audio manager.
/// </summary>
public class AudioManager : MonoBehavior
{
    public const string preferenceAudioMute = "preferenceAudioMute";

    [SerializeField] 
    private Dictionary<AudioClipName, Sound> sounds;

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup soundEffectMixerGroup;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(preferenceAudioMute))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(preferenceAudioMute);
        }


        foreach (KeyValuePair<AudioClipName, Sound> entry in sounds)
        {
            Sound s = entry.Value;

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;

            switch (s.audioType)
            {
                case AudioType.SoundEffect:
                    s.source.outputAudioMixerGroup = soundEffectMixerGroup;
                    break;

                case AudioType.MainMusic:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    private Sound GetSound(AudioClipName audioClipName)
    {
        Sound sound;
        if (!sounds.TryGetValue(audioClipName, out sound))
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
        Sound s = GetSound(audioClipName);
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
        Sound s = GetSound(audioClipName);
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