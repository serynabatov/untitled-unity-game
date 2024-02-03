using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRZJ : MonoBehaviour , IRevealable
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Reveal()
    {
        print("Here is DRZJ");
        sprite.enabled = true;
    }

    public void Conceal()
    {
        sprite.enabled = false;
    }
}
