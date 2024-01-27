using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private float speedMod;
    [SerializeField]
    private float rotationMod;
    [SerializeField]
    private float pushForce;

    private Vector2 _startingPosition;

    private Rigidbody2D rb;

    private void Start()
    {
        _startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        PlayerController2D.OnTrapActivated += TrapActivation;
        PlayerController2D.OnBoulderCollision += ResetPosition;
    }

    private void OnDestroy()
    {
        PlayerController2D.OnTrapActivated -= TrapActivation;
        PlayerController2D.OnBoulderCollision -= ResetPosition;
    }


    private void TrapActivation()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(Vector2.down * pushForce, ForceMode2D.Impulse);
    }

    private void ResetPosition()
    {
        transform.position = _startingPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
