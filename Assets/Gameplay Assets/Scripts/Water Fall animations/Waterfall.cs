using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour , IAnimationPlayable
{
    private Animator anim;
    private SpriteRenderer water;
    private AudioSource waterSourceSound;
    private int status;

    // Start is called before the first frame update
    void Start()
    {
        BeginPlaying();
    }

    public void BeginPlaying()
    {
        water = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        waterSourceSound = GetComponent<AudioSource>();

        status = PlayerPrefs.GetInt(GameManager.WaterLevelStatus);
        if (status == 1)
        {
            waterSourceSound.Play();
            water.enabled = true;
            anim.Play("Waterfall");
        }
        else
            water.enabled = false;
    }
}
