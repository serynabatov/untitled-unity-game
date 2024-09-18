using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 _position;

    public Vector2 Position { get { return _position; } }
}
