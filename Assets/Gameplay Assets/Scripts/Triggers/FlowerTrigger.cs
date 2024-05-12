using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowerTrigger : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField]
    private SpriteRenderer _flowerSprite;

    [SerializeField]
    private GameObject _fogPrefab;

    [SerializeField]
    private GameObject _trigger;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private float _delay;

    private bool _isInRange;

    public UnityEvent OnFlowerChange;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
    }

    private void FixedUpdate()
    {
        if (_isInRange&&_inputManager.GetInteractPressed())
        {
            SetFog();
            StartCoroutine(SetDelay(_delay));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _trigger.SetActive(true);
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _trigger.SetActive(false);
            _isInRange = false;
        }
    }

    private void SetFog()
    {
        _fogPrefab.SetActive(true);
    }


    private void ChangeSprite()
    {
        _flowerSprite.sprite = _sprite;
        Destroy(this);
    }

    private IEnumerator SetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnFlowerChange?.Invoke();
        ChangeSprite();
    }
}
