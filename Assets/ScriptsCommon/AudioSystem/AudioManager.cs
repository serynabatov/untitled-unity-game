
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



/// <summary>
/// Audio manager.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public const string preferenceAudioMute = "preferenceAudioMute";

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    Slider musicVolumeSlider;

    [SerializeField]
    private AudioMixerGroup soundEffectMixerGroup;

    [SerializeField]
    Slider soundEffectSlider;


    [SerializeField]
    private List<BasicSound> sounds;

    private ConcurrentDictionaryImpl concurrentSounds = ConcurrentDictionaryImpl.Instance;

    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    public static AudioManager Instance;

    private void Awake()
    {
        concurrentSounds.ClearDictionary();

        Instance = this;

        if (PlayerPrefs.HasKey(preferenceAudioMute))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(preferenceAudioMute);
        }

        if (PlayerPrefs.HasKey(Constants.preferenceAudioVolume))
        {
            musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat(Constants.preferenceAudioVolume) * 20));
            musicVolumeSlider.value = PlayerPrefs.GetFloat(Constants.preferenceAudioVolume);
        }

        if (PlayerPrefs.HasKey(Constants.preferenceSoundEffectsVolume))
        {
            soundEffectMixerGroup.audioMixer.SetFloat("SoundsEffect", Mathf.Log10(PlayerPrefs.GetFloat(Constants.preferenceSoundEffectsVolume) * 20));
            soundEffectSlider.value = PlayerPrefs.GetFloat(Constants.preferenceSoundEffectsVolume);
        }
        AddMusicToManage(sounds);
    }

    private void Start()
    {
        UpdateMixerVolume();
    }

    public void ResetSliders()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat(Constants.preferenceAudioVolume);
        soundEffectSlider.value = PlayerPrefs.GetFloat(Constants.preferenceSoundEffectsVolume);
    }

    private BasicSound GetSound(AudioClipName audioClipName)
    {
        BasicSound sound;
        if (!concurrentSounds.sounds.TryGetValue(audioClipName, out sound))
        {
            // nothing here
            return null;
        }
        return sound;
    }

    public void Play(int audioClipName, bool isSpecific = false, int clip = 0)
    {
        BasicSound s = GetSound((AudioClipName)audioClipName);
        if (s != null)
        {
            if (!isSpecific)
            {
                Play(audioClipName);
            }
            else
            {
                if (clip > 0)
                {
                    clip %= s.audioClip.Length;
                }
                s.audioSource.clip = s.audioClip[clip];
                s.audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Plays the specified audioClip.
    /// </summary>
    /// <param name="audioClipName">Audio clip name.</param>
    public void Play(int audioClipName)
    {
        BasicSound s = GetSound((AudioClipName)audioClipName);
        if (s != null)
        {
            if (s.audioClip.Length > 1)
            {
                int randomClip = UnityEngine.Random.Range(0, s.audioClip.Length);
                s.audioSource.clip = s.audioClip[randomClip];
            }
            s.audioSource.Play();
        }
    }


    public void Stop(int audioClipName, bool isChanging = false, int clip = 0)
    {
        BasicSound s = GetSound((AudioClipName)audioClipName);
        if (s != null)
        {
            if (!isChanging)
            {
                Stop(audioClipName);
            }
            else
            {
                if (clip > 0)
                {
                    clip %= s.audioClip.Length;
                }
                s.audioSource.clip = s.audioClip[clip];
                s.audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Stop the specified audioClipName.
    /// </summary>
    /// <param name="audioClipName">Audio clip name.</param>
    public void Stop(int audioClipName)
    {
        BasicSound s = GetSound((AudioClipName)audioClipName);
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
        soundEffectMixerGroup.audioMixer.SetFloat("SoundsEffect", Mathf.Log10(soundEffectSlider.value) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
    }


    /// <summary>
    /// Plays the specified sound.
    /// </summary>
    /// <param name="sound">Sound.</param>
    public void PlayTheSpecifiedSound()
    {
        Action<MessagePayload<int>> actionPlayTheSpecifiedSound = EventHandlerPlayTheSpecifiedSound;
        broker.Subscribe<int>(actionPlayTheSpecifiedSound);
    }

    private void EventHandlerPlayTheSpecifiedSound(MessagePayload<int> audio)
    {
        if (audio.stoping)
        {
            if (audio.payload >= 0)
            {
                Stop(audio.payload, audio.isChanging, audio.clipNumber);
            }
            else
            {
                return;
            }
        }
        else
        {
            if (audio.payload >= 0)
            {
                Play(audio.payload, audio.isChanging, audio.clipNumber);
            }
            else
            {
                return;
            }
        }
    }

    public void SetupMusic(BasicSound value)
    {
        BasicSound s = value;

        s.audioSource = gameObject.AddComponent<AudioSource>();

        if (s.audioClip.Length > 1)
        {
            int randomClip = UnityEngine.Random.Range(0, s.audioClip.Length - 1);
            s.audioSource.clip = s.audioClip[randomClip];
        }
        else
        {
            s.audioSource.clip = s.audioClip[0];
        }

        s.audioSource.loop = s.isLoop;
        s.audioSource.playOnAwake = s.playOnAwake;
        s.audioSource.minDistance = s.minDistance;
        s.audioSource.maxDistance = s.maxDistance;
        s.audioSource.spatialBlend = s.spatialBlend;
        s.audioSource.volume = s.volume;

        s.audioSource.rolloffMode = s.AudioRolloffMode;

        if ((int)s.mixerGroup == 0)
        {
            s.audioSource.outputAudioMixerGroup = musicMixerGroup;
        }
        else if ((int)s.mixerGroup == 1)
        {
            s.audioSource.outputAudioMixerGroup = soundEffectMixerGroup;
        }

        if (s.playOnAwake)
        {
            s.audioSource.Play();
        }
    }

    public void AddMusicToManage(List<BasicSound> soundsToAdd)
    {
        if (!concurrentSounds.sounds.IsEmpty)
        {
            foreach (KeyValuePair<AudioClipName, BasicSound> entry in concurrentSounds.sounds)
            {
                SetupMusic(entry.Value);
            }
        }

        for (int i = 0; i < soundsToAdd.Count; i++)
        {
            concurrentSounds.sounds[soundsToAdd[i].audioClipName] = soundsToAdd[i];
            AudioManager.Instance.SetupMusic(soundsToAdd[i]);
        }

        PlayTheSpecifiedSound();

    }
}