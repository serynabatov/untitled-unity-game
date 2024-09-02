using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsScript : MonoBehaviour
{
    public event Action<Vector2> OnCheckpointTrigger;

    [SerializeField]
    private Vector2 _checkPoinPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnCheckpointTrigger?.Invoke(_checkPoinPosition);
        }
    }
}
