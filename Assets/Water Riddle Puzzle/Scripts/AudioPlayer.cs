using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _clipList;

    private AudioSource _audioSource;

    private static int _index;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        print(_index);
        if (!_audioSource.isPlaying)
        {
            ChangeSoundClip();
        }
    }

    public void ChangeSoundClip()
    {
        _index += 1;
        _index %= _clipList.Count;
        _audioSource.clip = _clipList[_index];
        PlaySound();
    }

    public void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
