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

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
    }
}
