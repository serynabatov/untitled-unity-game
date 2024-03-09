using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealCrystalRoom : MonoBehaviour
{
    [SerializeField]
    private BlockerBehavior _block;

    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _block.OnBlockerDisable += DisableForegroundSprite;
    }

    private void OnDestroy()
    {
        _block.OnBlockerDisable -= DisableForegroundSprite;
    }

    private void DisableForegroundSprite()
    {
        _sprite.enabled = false;
        _block.OnBlockerDisable -= DisableForegroundSprite;
    }
}
