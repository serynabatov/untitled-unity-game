using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Locations
{
    Upper = 0,
    Middle = 1,
    Lower = 2
}

public enum LocationType
{
    Forest = 0,
    Cave = 1
}

public class LocationCheck : MonoBehaviour
{
    [SerializeField]
    private Locations _currentLocation;

    [SerializeField]
    private LocationType _currentLocationType;

    public LocationType CurrentLocationType {  get { return _currentLocationType; } }

    public Locations CurrentLocation { get { return _currentLocation; } }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }


    [SerializeField]
    private GameObject location;
    [SerializeField]
    private string iD;

    public void ChangeLocationName()
    {
        Localizator localizator = location.GetComponent<Localizator>();
        Animator anim = location.GetComponent<Animator>();
        localizator.iD = iD;
        anim.SetTrigger("RevealLocation");
    }
}
