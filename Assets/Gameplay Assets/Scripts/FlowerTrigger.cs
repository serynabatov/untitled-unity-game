using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowerTrigger : MonoBehaviour
{
    private InputManager _inputManager;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private GameObject _fogPrefab;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private float _delay;

    private bool _isInRange;

    public UnityEvent OnFlowerChange;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isInRange&&_inputManager.GetInteractPressed())
        {
            OnFlowerChange?.Invoke();
            SetFog();
            StartCoroutine(SetDelay(_delay));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }

    private void SetFog()
    {
        _fogPrefab.SetActive(true);
    }

    private void RemoveFog()
    {
        _fogPrefab.SetActive(false);
    }

    private void ChangeSprite()
    {
        _spriteRenderer.sprite = _sprite;
        Destroy(this);
    }

    private IEnumerator SetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeSprite();
        RemoveFog();
    }
}
