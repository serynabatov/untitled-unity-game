using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellWater : MonoBehaviour, IAnimationPlayable
{
    private Animator anim;
    private int status;
    // Start is called before the first frame update
    void Start()
    {
        BeginPlaying();
    }

    public void BeginPlaying()
    {
        anim = GetComponent<Animator>();
        status = PlayerPrefs.GetInt("Water level status");
        if (status == 1)
        {
            anim.Play("Well");
        }
        else
        {
            anim.Play("WellDefault");
        }
    }
}
