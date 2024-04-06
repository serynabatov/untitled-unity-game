using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotationCorrection : MonoBehaviour
{
    private bool _active = true;

    public bool Active { get { return _active; } set { _active = value; } }

    void LateUpdate()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();

        if (moveDirection != Vector2.zero && _active == true)
        {
            CorrectPlayerRotation();
        }
    }

    private float GetCurrentAngle()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        return Vector2.SignedAngle(Vector2.up, moveDirection);
    }

    private void CorrectPlayerRotation()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, GetCurrentAngle());
        transform.rotation = rotation;
    }
}
