using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Action Move;
    private Action Input;

    public static event Action OnTrapActivated;
    public static event Action OnBoulderCollision;
    public static event Action OnRespawn;
    public static event Action OnDamaged;

    public static int currentLocation;

    [SerializeField]
    private Timer onTimer;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float airborneMovementSpeed;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private float checkDistance;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    private SpriteRenderer playerSprite;
    private Animator animator;

    [SerializeField] private float startTimerValue;
    private float xInput;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private float timer;

    private int facingDirection = 1;

    private bool isGrounded;
    private bool isOnSlope;
    private bool slopeOnSide;
    private bool isJumping;
    private bool canWalkOnSlope;
    private bool canJump;
    private bool jumpIsBuffered;

    private bool _isMusicStoppng;

    private Vector2 newVelocity;
    private Vector2 newForce;

    private Vector2 slopeNormalPerp;
    [SerializeField] private Vector2 savedPosition;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc;

    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    private void Start()
    {
        SaveSystem.SaveCurrentScene();

        SceneSystem.OnSceneChange += CannotStopMusic;

        DebugScript.OnSceneReset += CannotStopMusic;

        DialogueManager.DialogueStarted += RemovingControl;
        DialogueManager.DialogueEnded += ResumingControl;

        transform.position = SaveSystem.LoadPosition();
        timer = startTimerValue;

        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        Input += CheckInput;
        Move += ApplyMovement;
        savedPosition = transform.position;

        _isMusicStoppng = true;
    }

    private void OnDestroy()
    {
        SceneSystem.OnSceneChange -= CannotStopMusic;

        DebugScript.OnSceneReset -= CannotStopMusic;

        DialogueManager.DialogueStarted -= RemovingControl;
        DialogueManager.DialogueEnded -= ResumingControl;

    }

    private void Update()
    {
        Input?.Invoke();
    }

    private void FixedUpdate()
    {
        BufferedJump();
        CheckGround();
        SlopeCheck();
        Move?.Invoke();
    }

    private void CheckInput()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || PauseManager.paused)
        {
            return;
        }
        xInput = InputManager.GetInstance().GetMoveAxis();
        animator.SetFloat("VelocityX", Math.Abs(xInput));
        if (xInput != facingDirection && xInput != 0)
        {
            Flip();
        }
        if (InputManager.GetInstance().GetJumpPressed())
        {
            BufferedJumpCheck();
            Jump();
        }

    }
    private void CheckGround()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("VelocityY", rb.velocity.y);
        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
            return;
        }

        if (!isGrounded)
        {
            canJump = false;
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = groundCheck.position;

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        Debug.DrawLine(checkPos, new Vector3(checkPos.x + slopeCheckDistance, checkPos.y), Color.red);
        Debug.DrawLine(checkPos, new Vector3(checkPos.x - slopeCheckDistance, checkPos.y), Color.red);

        if (slopeHitFront)
        {
            slopeOnSide = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            slopeOnSide = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            slopeOnSide = false;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        Debug.DrawLine(checkPos, new Vector3(checkPos.x, checkPos.y - slopeCheckDistance), Color.green);

        if (Vector2.Angle(hit.normal, Vector2.up) <= 10)
        {
            isOnSlope = false;
        }
        else if (slopeOnSide)
        {
            isOnSlope = true;
        }

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }
        if (isGrounded && xInput == 0.0f) //раньше было (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void Jump()
    {
        if (canJump)
        {

            animator.SetTrigger("Jump");
            canJump = false;
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
            jumpIsBuffered = false;
        }
    }

    private void BufferedJump()
    {
        if (jumpIsBuffered)
        {
            Jump();
        }
    }

    private void BufferedJumpCheck()
    {
        float rayLength;
        if (Mathf.Sign(rb.velocity.y) == -1)
        {
            rayLength = checkDistance;
        }
        else
        {
            rayLength = 0.0f;
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, rayLength, whatIsGround);

        if (!isGrounded && hit)
        {
            jumpIsBuffered = true;
        }
    }

    private void ApplyMovement()
    {
        if (isGrounded && !isOnSlope && !isJumping) //if not on slope
        {
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping) //If on slope
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded) //If in air
        {
            newVelocity.Set(airborneMovementSpeed * xInput, rb.velocity.y);
            rb.velocity = newVelocity;
        }

    }

    private void Flip()
    {
        facingDirection *= -1;
        playerSprite.flipX = !playerSprite.flipX;
    }

    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity);
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(facingDirection, 1, 1);
        }
    }
    public void OnApplicationQuit()
    {
        SaveSystem.SavePosition(transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void TakeDamage()
    {
        OnDamaged?.Invoke();
        RemovingControl();
        animator.SetTrigger("Damaged");
        xInput = 0;
        rb.velocity = new Vector2(0, 0);
    }

    private void Respawn()
    {
        OnRespawn?.Invoke();
        animator.SetFloat("VelocityX", 0);
        transform.position = savedPosition;
        ResumingControl();

    }

    public void RemovingControl()
    {
        if (animator != null)
        {
            animator.SetFloat("VelocityX", 0);
        }
        xInput = 0;
        Input -= CheckInput;
    }

    private void ResumingControl()
    {
        onTimer.SetTimer(0.4f, () => { Input += CheckInput; });
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                HazardBehavior hazard = collision.GetComponent<HazardBehavior>();
                hazard.PlayHazardSound();
                break;
            case "Trap":
                OnTrapActivated?.Invoke();
                break;
            case "Location":
                LocationCheck locationCheck = collision.gameObject.GetComponent<LocationCheck>();
                LocationMusicChanger locationMusicChanger = collision.gameObject.GetComponent<LocationMusicChanger>();

                currentLocation = (int)locationCheck.CurrentLocation;
                locationCheck.ChangeLocationName();

                if (locationCheck.SpriteRenderer != null)
                {
                    SpriteRenderer locationConceal = locationCheck.SpriteRenderer;
                    locationConceal.enabled = false;
                }

                locationMusicChanger.StartMusic();

                broker.Publish<int>((int)AudioClipName.Ambience, false, true, (int)locationCheck.CurrentLocationType);
                break;
            case "Revealable":
                if (collision.gameObject.name == "DRZ")
                {
                    Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
                }
                broker.Publish<int>((int)AudioClipName.UnveilSound);

                IRevealable reveal = collision.gameObject.GetComponent<IRevealable>();
                reveal.Reveal();
                break;
            case "Checkpoint":
                collision.TryGetComponent(out CheckpointScript checkpointScript);
                if (checkpointScript != null)
                {
                    savedPosition = checkpointScript.Position;
                }
                break;
        }
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            if (timer > 0)
            {
                if (timer == startTimerValue)
                {
                    TakeDamage();
                }

                timer -= Time.deltaTime;
            }
            else
            {
                Respawn();
                timer = startTimerValue;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Revealable"))
        {
            IRevealable reveal = collision.gameObject.GetComponent<IRevealable>();
            reveal.Conceal();
        }

        if (collision.gameObject.CompareTag("Location"))
        {
            LocationCheck locationCheck = collision.gameObject.GetComponent<LocationCheck>();
            LocationMusicChanger locationMusicChanger = collision.gameObject.GetComponent<LocationMusicChanger>();

            if (_isMusicStoppng)
            {
                locationMusicChanger.StopMusic();
            }


            if (locationCheck.SpriteRenderer != null)
            {
                SpriteRenderer locationConceal = locationCheck.SpriteRenderer;
                locationConceal.enabled = true;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();
            timer = startTimerValue;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            OnBoulderCollision?.Invoke();
            broker.Publish<int>((int)AudioClipName.Hurt);
            Respawn();
        }
    }

    private void CannotStopMusic()
    {
        _isMusicStoppng = false;
    }
}
