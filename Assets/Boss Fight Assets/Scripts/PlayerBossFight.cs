using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossFight : MonoBehaviour
{

    [Header("Movement Params")]
    public float runSpeed = 5.0f;
    public float jumpForce = 5.0f;

    [SerializeField]
    private Transform groundCheck;

    private string currentState;

    private bool isAttacking = false;
    private bool isGrounded = false;
    private bool isRunning = false;

    private BoxCollider2D coll;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    //Animation states
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_ATTACK1 = "Attack1";
    const string PLAYER_ATTACK2 = "Attack2";
    const string PLAYER_RUN = "Run";
    const string PLAYER_RUN_START = "RunStart";
    const string PLAYER_RUN_STOP = "RunStop";
    const string PLAYER_JUMP = "Jump";




    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Move Vector 2 = " + InputManager.GetInstance().GetMoveDirection());
        //Debug.Log("Move Axis = " + InputManager.GetInstance().GetMoveAxis());
        //Debug.Log("UpAttack" + InputManager.GetInstance().GetUpAttackPressed());
        /*
        if (InputManager.GetInstance().GetAttack1Pressed() && !isRunning)
        {
            Debug.Log("Attack 1 Pressed");
            isAttacking = true;
            ChangeAnimationState(PLAYER_ATTACK1);
        }
        if (InputManager.GetInstance().GetAttackCancelled() && !isRunning)
        {
            Debug.Log("Attack Cancelled");
            AttackFinished();
        } */
        
    }

    private void FixedUpdate()
    {
        /*
        //Checks if player on the ground or not
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ? true : false;
        //Debug.Log("isGrounded = " + isGrounded);

        HandleMovement();

        if (isAttacking && !isRunning)
        {
            //ChangeAnimationState(PLAYER_ATTACK1);
            if (InputManager.GetInstance().GetAttack2Pressed())
            {
                Debug.Log("Attack 2 Pressed");
                ChangeAnimationState(PLAYER_ATTACK2);
            }
        }
        if (InputManager.GetInstance().GetJumpPressed() && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            ChangeAnimationState(PLAYER_JUMP);
        }
        */
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        rb.velocity = new Vector2(moveDirection.x * runSpeed, rb.velocity.y);

        switch (moveDirection.x)
        {
            case 0:
                isRunning = false;
                if (!isAttacking && isGrounded)
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
                break;
            case 1:
                spriteRenderer.flipX = false;
                isRunning = true;
                if (!isAttacking && isGrounded)
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
                break;
            case -1:
                spriteRenderer.flipX = true;
                isRunning = true;
                if (!isAttacking && isGrounded)
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
                break;
        }
    }

    public void AttackFinished()
    {
        isAttacking = false;
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState)
        {
            return;
        }
        else
        {
            //play the animation
            animator.Play(newState);
            //reassign the current state
            currentState = newState;
        }
    }
}
