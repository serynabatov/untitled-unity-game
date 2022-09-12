
using System;
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
    private CustomMap<AudioClipName, BasicSound> sounds;

    private ConcurrentDictionaryImpl concurrentSounds = ConcurrentDictionaryImpl.Instance;

    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    public static AudioManager Instance;

    private void Awake()
    {
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
                int randomClip = UnityEngine.Random.Range(0, s.audioClip.Length - 1);
                s.audioSource.clip = s.audioClip[randomClip];
            }
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
        soundEffectMixerGroup.audioMixer.SetFloat("SoundsEffect", Mathf.Log10(AudioOptionsManager.soundsEffectVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
    }

    /// <summary>
    /// If the user touches the object
    /// </summary>
    /// <param name="other">Other.</param>
    public void OnTriggerEnter(Collider other)
    {
        // TODO: play the sound on collision
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
        if (audio.payload >= 0)
        {
            Play(audio.payload);
        }
        else
        {
            return;
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
        s.audioSource.volume = s.volume;

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

    // TODO: make the automatic key generation 
    public void AddMusicToManage(CustomMap<AudioClipName, BasicSound> soundsToAdd)
    {
        if (!concurrentSounds.sounds.IsEmpty)
        {
            foreach (KeyValuePair<AudioClipName, BasicSound> entry in concurrentSounds.sounds)
            {
                SetupMusic(entry.Value);
            }
        }

        for (int i = 0; i < soundsToAdd.KeysList.Count; i++)
        {
            concurrentSounds.sounds[soundsToAdd.KeysList[i]] = soundsToAdd.ValuesList[i];
            AudioManager.Instance.SetupMusic(soundsToAdd.ValuesList[i]);
        }


        PlayTheSpecifiedSound();

    }
}