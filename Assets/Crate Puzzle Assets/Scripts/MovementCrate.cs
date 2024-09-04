using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class MovementCrate : MonoBehaviour
{
    [SerializeField]
    private List<CheckpointsScript> _checkpointsScripts;

    [Header("Movement Params")]
    public float runSpeedX = 5.0f;
    public float runSpeedY = 5.0f;

    [Header("Grab objects")]
    public Transform boxHolderLeft;
    public Transform boxHolderRight;
    public Transform boxHolderUp;
    public Transform boxHolderDown;

    [SerializeField]
    private InputActionReference interact;

    [SerializeField]
    private GameObject _sprite;

    public Transform grabDetect;

    private TMP_Text tmp;

    public float rayDist;

    private bool interactPress = true;

    private GameObject boxTemp;
    private Transform boxHolderTemp;
    private SpriteRotationCorrection _spriteRotation;

    private MessageBrokerImpl broker;

    private Rigidbody2D rb;

    public LayerMask grabAble;

    private bool _soundPlaying;


    private void Awake()
    {
        transform.position = SaveSystem.LoadPosition();
        tmp = GetComponentInChildren<TMP_Text>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _checkpointsScripts[0].OnCheckpointTrigger += SaveCheckpointPosition;
        _checkpointsScripts[1].OnCheckpointTrigger += SaveCheckpointPosition;
        _checkpointsScripts[2].OnCheckpointTrigger += SaveCheckpointPosition;

        CursorScript.HideCursor();

        SaveSystem.SaveCurrentScene();

        broker = MessageBrokerImpl.Instance;
        _spriteRotation = _sprite.GetComponent<SpriteRotationCorrection>();
    }

    private void OnDestroy()
    {
        _checkpointsScripts[0].OnCheckpointTrigger -= SaveCheckpointPosition;
        _checkpointsScripts[1].OnCheckpointTrigger -= SaveCheckpointPosition;
        _checkpointsScripts[2].OnCheckpointTrigger -= SaveCheckpointPosition;
    }
    private void FixedUpdate()
    {

        CrateMovementSound();
        HandleMovement();
        RevealInteractiveCue();

        RaycastHit2D grabCheckLeft = Physics2D.Raycast(grabDetect.position, Vector2.left, rayDist, grabAble);
        RaycastHit2D grabCheckRight = Physics2D.Raycast(grabDetect.position, Vector2.right, rayDist, grabAble);
        RaycastHit2D grabCheckUp = Physics2D.Raycast(grabDetect.position, Vector2.up, rayDist, grabAble);
        RaycastHit2D grabCheckDown = Physics2D.Raycast(grabDetect.position, Vector2.down, rayDist, grabAble);

        if (InputManager.GetInstance().GetInteractPressed())
        {
            //Debug.Log("Get instance");
            if (this.interactPress)
            {

                if (grabCheckLeft.collider != null)
                {
                    this.GrabSex(grabCheckLeft, boxHolderLeft, grabCheckLeft.collider.tag);
                    this.interactPress = false;
                }
                else if (grabCheckRight.collider != null)
                {
                    this.GrabSex(grabCheckRight, boxHolderRight, grabCheckRight.collider.tag);
                    this.interactPress = false;
                }
                else if (grabCheckUp.collider != null)
                {
                    this.GrabSex(grabCheckUp, boxHolderUp, grabCheckUp.collider.tag);
                    this.interactPress = false;
                }
                else if (grabCheckDown.collider != null)
                {
                    this.GrabSex(grabCheckDown, boxHolderDown, grabCheckDown.collider.tag);
                    this.interactPress = false;
                }
            }
            else
            {
                runSpeedX = 5f;
                runSpeedY = 5f;
                this.interactPress = true;
                boxTemp.transform.SetParent(null);
                boxTemp = null;
                boxHolderTemp = null;
                _spriteRotation.Active = true;
            }
        }
    }

    private void GrabSex(RaycastHit2D grabCheck, Transform boxHolder, string tag)
    {
        switch (tag)
        {
            case "GreenBox":
                runSpeedX = 6f;
                runSpeedY = 6f;
                break;
            case "YellowBox":
                runSpeedX = 6f;
                runSpeedY = 6f;
                break;
            case "IceBox":
                runSpeedX = 6f;
                runSpeedY = 6f;
                break;
            case "FireBox":
                runSpeedX = 6f;
                runSpeedY = 6f;
                break;
        }
        broker.Publish(18);

        boxTemp = grabCheck.collider.gameObject;
        boxHolderTemp = boxHolder;

        boxTemp.transform.position = boxHolderTemp.position;
        boxTemp.transform.SetParent(boxHolderTemp);

        _spriteRotation.Active = false;
    }


    private void HandleMovement()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        rb.velocity = new Vector2(moveDirection.x * runSpeedX, moveDirection.y * runSpeedY);

    }
    private void RevealInteractiveCue()
    {
        RaycastHit2D grabCheckLeft = Physics2D.Raycast(grabDetect.position, Vector2.left, rayDist, grabAble);
        RaycastHit2D grabCheckRight = Physics2D.Raycast(grabDetect.position, Vector2.right, rayDist, grabAble);
        RaycastHit2D grabCheckUp = Physics2D.Raycast(grabDetect.position, Vector2.up, rayDist, grabAble);
        RaycastHit2D grabCheckDown = Physics2D.Raycast(grabDetect.position, Vector2.down, rayDist, grabAble);

        if ((grabCheckLeft || grabCheckRight || grabCheckUp || grabCheckDown) && (interactPress))
        {
            tmp.text = InputControlPath.ToHumanReadableString(interact.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            tmp.enabled = true;
        }
        else if ((!grabCheckLeft && !grabCheckRight && !grabCheckUp && !grabCheckDown) || (!interactPress))
        {
            tmp.enabled = false;
        }
    }

    private void CrateMovementSound()
    {
        if (transform.hasChanged && !interactPress)
        {
            if (!_soundPlaying)
            {
                broker.Publish<int>((int)AudioClipName.CrateMovement);
                _soundPlaying = true;
            }
            transform.hasChanged = false;
        }
        else
        {
            broker.Publish<int>((int)AudioClipName.CrateMovement, true);
            _soundPlaying = false;
        }
    }

    private void SaveCheckpointPosition(Vector2 position)
    {
        SaveSystem.SavePosition(position);
    }
}
