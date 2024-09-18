using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public static event Action OnBoulderEnd;

    [SerializeField]
    private float pushForce;

    [SerializeField]
    [Range(0f, 5f)]
    private float acceleration;

    private bool _isChasing;

    private Vector2 _startingPosition;

    private Quaternion _startingRotation;

    private Rigidbody2D rb;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _boulderShadow;
    [SerializeField]
    private GameObject _boulderEffect;

    [SerializeField]
    private LocationMusicChanger _location;

    private SpriteRenderer _shadowSprite;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    private void Start()
    {
        _startingPosition = transform.position;

        _startingRotation = _boulderShadow.transform.rotation;

        rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _shadowSprite = _boulderShadow.GetComponent<SpriteRenderer>();

        _isChasing = false;

        PlayerController2D.OnTrapActivated += TrapActivation;
        PlayerController2D.OnTrapActivated += StartBoulderSound;
        PlayerController2D.OnBoulderCollision += StopBoulderSound;
        PlayerController2D.OnBoulderCollision += ResetPosition;

        PauseManager.OnPause += StopSfxOnPause;
        PauseManager.OnResume += ResumeSfxOnResume;

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

        PauseManager.OnPause -= StopSfxOnPause;
        PauseManager.OnResume -= ResumeSfxOnResume;

        OnBoulderEnd -= BoulderEnd;
        OnBoulderEnd -= StopBoulderSound;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * acceleration, ForceMode2D.Force);
        ShadowRotationBlock();

    }

    private void TrapActivation()
    {
        PlayerController2D.OnRespawn += ResetPosition;
        PlayerController2D.OnRespawn += StopBoulderSound;

        _circleCollider.enabled = true;
        _spriteRenderer.enabled = true;
        _shadowSprite.enabled = true;

        _isChasing = true;

        _broker.Publish<int>((int)AudioClipName.BoulderLanding);

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

        _isChasing = false;

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
        if (!_isChasing)
        {
            _location.JustStop();
            _audioSource?.Play();
            _broker.Publish<int>((int)AudioClipName.BoulderMove);
        }
    }

    private void StopBoulderSound()
    {
        _location.JustStart();
        _audioSource?.Stop();
        _broker.Publish<int>((int)AudioClipName.BoulderMove, true);
    }

    private void ShadowRotationBlock()
    {
        _boulderShadow.transform.rotation = _startingRotation;
    }

    private void BoulderEnd()
    {
        PlayerPrefs.SetInt("BoulderStatus", 1);
        _boulderEffect.SetActive(true);
        StartCoroutine(BoulderShrink());
    }

    private IEnumerator BoulderShrink()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = transform.localScale - new Vector3(Time.deltaTime, Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    private void StopSfxOnPause()
    {
        _broker.Publish<int>((int)AudioClipName.BoulderMove, true);
    }

    private void ResumeSfxOnResume()
    {
        _broker.Publish<int>((int)AudioClipName.BoulderMove);
    }

}
