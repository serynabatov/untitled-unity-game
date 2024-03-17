using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class ConcurrentDictionaryImpl
{
    private static ConcurrentDictionaryImpl _instance;

    public ConcurrentDictionary<AudioClipName, BasicSound> sounds;

    public static ConcurrentDictionaryImpl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ConcurrentDictionaryImpl();
            }
            return _instance;
        }
    }

    public ConcurrentDictionaryImpl()
    {
        sounds = new ConcurrentDictionary<AudioClipName, BasicSound>();
    }

    public void ClearDictionary()
    {
        sounds.Clear();
    }

    public void FillSounds(List<BasicSound> values)
    {
        if (AudioManager.Instance == null)
        {
            for (int i = 0; i < values.Count; i++)
            {
                this.sounds[values[i].audioClipName] = values[i];
            }
        }
        else
        {
            for (int i = 0; i < values.Count; i++)
            {
                this.sounds[values[i].audioClipName] = values[i];
                AudioManager.Instance.SetupMusic(values[i]);
            }
        }
    }
}

