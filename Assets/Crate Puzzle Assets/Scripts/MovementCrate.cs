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

    // components attached to player
    private CircleCollider2D coll;

    private Rigidbody2D rb;

    public LayerMask grabAble;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
            // Debug.Log("Get instance");

            if (grabCheckLeft.collider != null && grabCheckLeft.collider.tag == "Box")
            {
               // runSpeedY = 0;
                this.GrabSex(grabCheckLeft, boxHolderLeft);
            }

            if (grabCheckRight.collider != null && grabCheckRight.collider.tag == "Box")
            {
                //runSpeedY = 0;
                this.GrabSex(grabCheckRight, boxHolderRight);
            }

            if (grabCheckUp.collider != null && grabCheckUp.collider.tag == "Box")
            {
               // runSpeedX = 0;
                this.GrabSex(grabCheckUp, boxHolderUp);
            }

            if (grabCheckDown.collider != null && grabCheckDown.collider.tag == "Box")
            {
               // runSpeedX = 0;
                this.GrabSex(grabCheckDown, boxHolderDown);
            }
        }
    }

    private void GrabSex(RaycastHit2D grabCheck, Transform boxHolder)
    {
        //Debug.Log(interactPress);

        if (this.interactPress)
        {
            this.interactPress = false;
            grabCheck.collider.gameObject.transform.parent = boxHolder;
            grabCheck.collider.gameObject.transform.position = boxHolder.position;

        }
        else
        {
           // runSpeedX = 5f;
            //runSpeedY = 5f;
            this.interactPress = true;
            grabCheck.collider.gameObject.transform.parent = null;
        }
    }


    private void HandleMovement()
    {

        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        //Debug.Log(moveDirection);
        rb.velocity = new Vector2(moveDirection.x * runSpeedX, moveDirection.y * runSpeedY);
    }
}
