using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrate : MonoBehaviour
{
    [Header("Movement Params")]
    public float runSpeedX = 5.0f;
    public float runSpeedY = 5.0f;

    [Header("Grab objects")]
    public Transform boxHolderLeft;
    public Transform boxHolderRight;
    public Transform boxHolderUp;
    public Transform boxHolderDown;

    public Transform grabDetect;
    public float rayDist;

    private bool interactPress = true;

    private GameObject boxTemp;
    private Transform boxHolderTemp;

    // components attached to player
    private CircleCollider2D coll;

    private Rigidbody2D rb;

    public LayerMask grabAble;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //SaveSystem.GetInstance().SavePosition(transform.position);
        transform.position = SaveSystem.LoadPosition();
    }
    public void OnApplicationQuit()
    {
        SaveSystem.SavePosition(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    private void FixedUpdate()
    {
        HandleMovement();

        RaycastHit2D grabCheckLeft = Physics2D.Raycast(grabDetect.position, Vector2.left, rayDist, grabAble);
        RaycastHit2D grabCheckRight = Physics2D.Raycast(grabDetect.position, Vector2.right, rayDist, grabAble);
        RaycastHit2D grabCheckUp = Physics2D.Raycast(grabDetect.position, Vector2.up, rayDist, grabAble);
        RaycastHit2D grabCheckDown = Physics2D.Raycast(grabDetect.position, Vector2.down, rayDist, grabAble);

        if (InputManager.GetInstance().GetInteractPressed())
        {
            //Debug.Log("Get instance");
            if (this.interactPress)
            {
                this.interactPress = false;

                if (grabCheckLeft.collider != null)
                {
                    this.GrabSex(grabCheckLeft, boxHolderLeft, grabCheckLeft.collider.tag);
                }
                else if (grabCheckRight.collider != null)
                {
                    this.GrabSex(grabCheckRight, boxHolderRight, grabCheckRight.collider.tag);
                }
                else if (grabCheckUp.collider != null)
                {
                    this.GrabSex(grabCheckUp, boxHolderUp, grabCheckUp.collider.tag);
                }
                else if (grabCheckDown.collider != null)
                {
                    this.GrabSex(grabCheckDown, boxHolderDown, grabCheckDown.collider.tag);
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

        boxTemp = grabCheck.collider.gameObject;
        boxHolderTemp = boxHolder;

        boxTemp.transform.position = boxHolderTemp.position;
        boxTemp.transform.SetParent(boxHolderTemp);
    }


    private void HandleMovement()
    {
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        rb.velocity = new Vector2(moveDirection.x * runSpeedX, moveDirection.y * runSpeedY);

    }

}
