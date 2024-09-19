using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{

    MessageBrokerImpl broker = MessageBrokerImpl.Instance;
    private enum hazardType
    {
        spikes = 0,
        mushrooms = 1
    }

    [SerializeField]
    private hazardType hazard;

    public void PlayHazardSound()
    {
        switch (hazard)
        {
            case hazardType.spikes:
                broker.Publish<int>((int)AudioClipName.SpikeHurtSound);
                break;
            case hazardType.mushrooms:
                broker.Publish<int>((int)AudioClipName.MushroomHurtSound);
                break;
        }
    }
}
