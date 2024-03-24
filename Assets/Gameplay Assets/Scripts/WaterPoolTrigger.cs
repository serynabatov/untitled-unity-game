using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPoolTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _trigger;

    private bool _isInRange;

    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
    }

    private void FixedUpdate()
    {
        if (_isInRange&&_inputManager.GetInteractPressed())
        {

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = true;
            _trigger.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isInRange = false;
            _trigger.SetActive(false);
        }
    }
}
