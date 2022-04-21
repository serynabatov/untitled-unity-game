using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.
/*
1. Move "AD"
2. Jump "Space"
3. Attack "Left mouse click"
4. Interact dialogue/interaction(взаимодействие) "E"
5. Interact menu "ESC"
6. Interact inventory "I"
7. Guard/Parry "Right mouse click"
8. Submit "Space"
9. Skill "Q"
 */

//[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private float moveAxis = 0f;
    private bool jumpPressed = false;
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool attackPressed = false;
    private bool airAttackPressed = false;
    private bool parryPressed = false;
    private bool dodgePressed = false;
    private bool upAttackPressed = false;
    private bool airUpAttackPressed = false;
    private bool airSlamPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void MoveAxisPressed(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<float>();
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackPressed = true;
            airAttackPressed = true;
        }
        else if (context.canceled)
        {
            attackPressed = false;
            airAttackPressed = false;
        }
    }

    public void ParryPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            parryPressed = true;
        }
        else if (context.canceled)
        {
            parryPressed = false;
        }
    }

    public void DodgePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dodgePressed = true;
        }
        else if (context.canceled)
        {
            dodgePressed = false;
        }
    }

    public void UpAttackPressed(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            airUpAttackPressed = true;
        } */
        if (context.performed)
        {
            airUpAttackPressed = true;
            upAttackPressed = true;
        }
        else if (context.canceled)
        {
            upAttackPressed = false;
            airUpAttackPressed = false;
        }
    }

    public void AirSlamPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            airSlamPressed = true;
        }
        else if (context.canceled)
        {
            airSlamPressed = false;
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public float GetMoveAxis()
    {
        return moveAxis;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetJumpPressed()
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }

    public bool GetAttackPressed()
    {
        bool result = attackPressed;
        attackPressed = false;
        return result;
    }

    public bool GetParryPressed()
    {
        bool result = parryPressed;
        parryPressed = false;
        return result;
    }

    public bool GetDodgePressed()
    {
        bool result = dodgePressed;
        dodgePressed = false;
        return result;
    }


    public bool GetUpAttackPressed()
    {
        bool result = upAttackPressed;
        upAttackPressed = false;
        return result;
    }

    public bool GetAirUpAttackPressed()
    {
        bool result = airUpAttackPressed;
        airUpAttackPressed = false;
        return result;
    }

    public bool GetAirAttackPressed()
    {
        bool result = airAttackPressed;
        airAttackPressed = false;
        return result;
    }

    public bool GetAirSlamPressed()
    {
        bool result = airSlamPressed;
        airSlamPressed = false;
        return result;
    }
}
