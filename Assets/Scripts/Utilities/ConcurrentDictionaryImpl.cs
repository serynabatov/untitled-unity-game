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

    public FillSounds(List<AudioClipName> keys, List<BasicSound> values)
    {
        if (AudioManager.Instance == null)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                concurrentDictionaryImpl.sounds[keys[i]] = values[i];
            }
            /*foreach (KeyValuePair<AudioClipName, BasicSound> entry in sounds.DictionaryData)
            {
                concurrentDictionaryImpl.sounds[entry.Key] = entry.Value;
            }*/
        }
        else
        {
            for (int i = 0; i < keys.Count; i++)
            {
                concurrentDictionaryImpl.sounds[keys[i]] = values[i];
                AudioManager.Instance.SetupMusic(values[i]);
            }
            /*foreach (KeyValuePair<AudioClipName, BasicSound> entry in sounds.DictionaryData)
            {
            }*/
        }
    }
}

