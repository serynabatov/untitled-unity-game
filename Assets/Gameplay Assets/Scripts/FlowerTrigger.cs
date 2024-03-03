using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowerTrigger : MonoBehaviour
{
    private InputManager _inputManager;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite _sprite;

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
            ChangeSprite();
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

    private void ChangeSprite()
    {
        _spriteRenderer.sprite = _sprite;
        Destroy(this);
    }
}
