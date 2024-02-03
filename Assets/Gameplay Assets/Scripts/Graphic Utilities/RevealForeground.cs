using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealForeground : MonoBehaviour, IRevealable
{
    private SpriteRenderer sprite;
    private BoxCollider2D box;

    private void Start()
    {
        sprite = GetComponentInParent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    public void Reveal()
    {
        box.enabled = false;
        StartCoroutine(DrzjFogReveal());
    }

    IEnumerator DrzjFogReveal()
    {
        Color color = sprite.color;
        float alpha = 1f;
        while (alpha >= 0)
        {
            color.a = alpha;
            sprite.color = color;
            alpha -= 0.05f;
            if (alpha <= 0.05)
            {
                StopCoroutine(DrzjFogReveal());
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Conceal()
    {

    }
}
