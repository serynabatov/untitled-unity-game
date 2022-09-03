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
}

