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
        StartCoroutine(DrzjFogReveal());
    }

    private IEnumerator DrzjFogReveal()
    {
        float i = 255;
        while (i >= 0)
        {
            sprite.color = new Color(255, 255, 255, i);
            i -= 1;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Conceal()
    {

    }
}
