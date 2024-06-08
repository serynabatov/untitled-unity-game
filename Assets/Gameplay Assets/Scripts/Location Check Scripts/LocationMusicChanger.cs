using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMusicChanger : MonoBehaviour
{
    private AudioSource _music;

    private bool _isStarting;
    private bool _isStoping;

    [SerializeField]
    private int _fadeDuration;

    public int FadeDuration { get { return _fadeDuration; } }

    private void Awake()
    {
        _music = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        _isStoping = false;
        StartCoroutine(StartingMusic(_fadeDuration));
    }

    public void StopMusic()
    {
        _isStarting = false;
        StartCoroutine(StopingMusic(_fadeDuration));
    }

    IEnumerator StopingMusic(int fadeDuration)
    {
        float volume = _music.volume;
        float volumeHolder;
        float timer = 0.0f;
        _isStoping = true;

        while (_isStoping)
        {
            volumeHolder = volume - timer / fadeDuration;
            timer += Time.deltaTime;
            _music.volume = volumeHolder;

            if (volumeHolder < 0.05f)
            {
                _music.volume = 0.0f;
                _isStoping = false;
                _music.Stop();
            }

            yield return null;
        }
    }

    IEnumerator StartingMusic(int fadeDuration)
    {
        if (!_music.isPlaying)
        {
            _music.Play();
        }
        _isStarting = true;

        float volume = _music.volume;
        float volumeHolder;
        float timer = 0.0f;

        while (_isStarting)
        {
            volumeHolder = volume + timer / fadeDuration;
            timer += Time.deltaTime;
            _music.volume = volumeHolder;

            if (volumeHolder >0.95f)
            {
                _music.volume = 1.0f;
                _isStarting = false;
            }

            yield return null;
        }

    }
}
