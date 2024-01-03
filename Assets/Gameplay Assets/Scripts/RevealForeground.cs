using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealForeground : MonoBehaviour, IRevealable
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
    }
    public void Reveal()
    {
        sprite.enabled = false;
    }

    public void Conceal()
    {

    }
}
