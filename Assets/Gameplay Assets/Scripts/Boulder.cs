using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public static event Action OnBoulderEnd;

    [SerializeField]
    private float pushForce;

    private Vector2 _startingPosition;

    private Quaternion _startingRotation;

    private Rigidbody2D rb;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    [SerializeField]
    private GameObject _boulderShadow;
    [SerializeField]
    private GameObject _boulderEffect;

    private SpriteRenderer _shadowSprite;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    private void Start()
    {
        _startingPosition = transform.position;

        _startingRotation = _boulderShadow.transform.rotation;

        rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _shadowSprite = _boulderShadow.GetComponent<SpriteRenderer>();

        PlayerController2D.OnTrapActivated += TrapActivation;
        PlayerController2D.OnTrapActivated += StartBoulderSound;
        PlayerController2D.OnBoulderCollision += StopBoulderSound;
        PlayerController2D.OnBoulderCollision += ResetPosition;

        OnBoulderEnd += BoulderEnd;
        OnBoulderEnd += StopBoulderSound;
    }

    private void OnDestroy()
    {
        PlayerController2D.OnTrapActivated -= TrapActivation;
        PlayerController2D.OnTrapActivated -= StartBoulderSound;
        PlayerController2D.OnBoulderCollision -= StopBoulderSound;
        PlayerController2D.OnBoulderCollision -= ResetPosition;
        PlayerController2D.OnRespawn -= ResetPosition;
        PlayerController2D.OnRespawn -= StopBoulderSound;

        OnBoulderEnd -= BoulderEnd;
        OnBoulderEnd -= StopBoulderSound;
    }

    private void LateUpdate()
    {
        rb.AddForce(Vector2.right, ForceMode2D.Force);
        ShadowRotationBlock();
    }

    private void TrapActivation()
    {
        PlayerController2D.OnRespawn += ResetPosition;
        PlayerController2D.OnRespawn += StopBoulderSound;

        _circleCollider.enabled = true;
        _spriteRenderer.enabled = true;
        _shadowSprite.enabled = true;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(Vector2.down * pushForce, ForceMode2D.Impulse);
    }

    private void ResetPosition()
    {
        PlayerController2D.OnRespawn -= ResetPosition;
        PlayerController2D.OnRespawn -= StopBoulderSound;

        _circleCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _shadowSprite.enabled = false;

        transform.position = _startingPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap End"))
        {
            OnBoulderEnd?.Invoke();
        }
    }

    private void StartBoulderSound()
    {
        _broker.Publish<int>((int)AudioClipName.MusicEffect, 0, false, true, 3);
        _broker.Publish<int>((int)AudioClipName.BoulderMove);
    }

    private void StopBoulderSound()
    {
        _broker.Publish<int>((int)AudioClipName.MusicEffect, 0, false, true, 2);
        _broker.Publish<int>((int)AudioClipName.BoulderMove, 0, true);
    }

    private void ShadowRotationBlock()
    {
        _boulderShadow.transform.rotation = _startingRotation;
    }

    private void BoulderEnd()
    {
        _boulderEffect.SetActive(true);
        StartCoroutine(BoulderShrink());
    }

    private IEnumerator BoulderShrink()
    {
        while (transform.localScale.x > 0) {
            transform.localScale = transform.localScale - new Vector3(Time.deltaTime, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
