using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private float rotationMod;
    [SerializeField]
    private float pushForce;

    private Vector2 _startingPosition;

    private Rigidbody2D rb;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    private void Start()
    {
        _startingPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();

        PlayerController2D.OnTrapActivated += TrapActivation;
        PlayerController2D.OnTrapActivated += StartBoulderSound;
        PlayerController2D.OnBoulderCollision += StopBoulderSound;
        PlayerController2D.OnBoulderCollision += ResetPosition;
    }

    private void OnDestroy()
    {
        PlayerController2D.OnTrapActivated -= TrapActivation;
        PlayerController2D.OnTrapActivated -= StartBoulderSound;
        PlayerController2D.OnBoulderCollision -= StopBoulderSound;
        PlayerController2D.OnBoulderCollision -= ResetPosition;
        PlayerController2D.OnRespawn -= ResetPosition;
        PlayerController2D.OnRespawn -= StopBoulderSound;
    }


    private void TrapActivation()
    {
        PlayerController2D.OnRespawn += ResetPosition;
        PlayerController2D.OnRespawn += StopBoulderSound;

        _circleCollider.enabled = true;
        _spriteRenderer.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(Vector2.down * pushForce, ForceMode2D.Impulse);
    }

    private void ResetPosition()
    {
        PlayerController2D.OnRespawn -= ResetPosition;
        PlayerController2D.OnRespawn -= StopBoulderSound;

        _circleCollider.enabled = false;
        _spriteRenderer.enabled = false;
        transform.position = _startingPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void StartBoulderSound()
    {
        _broker.Publish<int>((int)AudioClipName.MusicEffect);
    }

    private void StopBoulderSound()
    {
        _broker.Publish<int>((int)AudioClipName.MusicEffect, 0, true);
    }
}
