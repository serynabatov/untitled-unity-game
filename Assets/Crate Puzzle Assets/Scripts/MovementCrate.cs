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

            if (grabCheckLeft.collider != null)
            {
                // runSpeedY = 0; && grabCheckLeft.collider.tag == "Box"
                this.GrabSex(grabCheckLeft, boxHolderLeft, grabCheckLeft.collider.tag);
            }

            if (grabCheckRight.collider != null)
            {
                //runSpeedY = 0; && grabCheckRight.collider.tag == "Box"
                this.GrabSex(grabCheckRight, boxHolderRight, grabCheckRight.collider.tag);
            }

            if (grabCheckUp.collider != null)
            {
                // runSpeedX = 0; && grabCheckUp.collider.tag == "Box"
                this.GrabSex(grabCheckUp, boxHolderUp, grabCheckUp.collider.tag);
            }

            if (grabCheckDown.collider != null)
            {
                // runSpeedX = 0; && grabCheckDown.collider.tag == "Box"
                this.GrabSex(grabCheckDown, boxHolderDown, grabCheckDown.collider.tag);
            }
        }
    }

    private void GrabSex(RaycastHit2D grabCheck, Transform boxHolder, string tag)
    {
        //Debug.Log(interactPress);

        if (this.interactPress)
        {
            switch (tag)
            {
                case "GreenBox": 
                    runSpeedX = 3f;
                    runSpeedY = 3f;
                    break;
                case "YellowBox":
                    runSpeedX = 4f;
                    runSpeedY = 4f;
                    break;
                case "IceBox":
                    runSpeedX = 3f;
                    runSpeedY = 3f; 
                    break;
                case "FireBox":
                    runSpeedX = 3f;
                    runSpeedY = 3f;
                    break;
            }
            this.interactPress = false;
            grabCheck.collider.gameObject.transform.parent = boxHolder;
            grabCheck.collider.gameObject.transform.position = boxHolder.position;
        }
        else
        {
            runSpeedX = 5f;
            runSpeedY = 5f;
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
