using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    // The objects to which we drag our object
    public GameObject draggedObject;
    private bool moving;
    private bool finish;

    // indicates where our button was clicked
    private float startPosX;
    private float startPosY;

    private Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finish)
        {
            // moving the object (pipe)
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(
                            mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z
                );
            }
        }
    }

    private void OnMouseUp()
    {
        moving = false;

        // if the distance between our dragged object and the square is less that 0.5
        // TODO: it won't work in our case; we need to think about how exactly we should work with start to work script
        // right now it is hardcoded
        if (Mathf.Abs(this.transform.position.x - draggedObject.transform.position.x) <= 0.8f &&
            Mathf.Abs(this.transform.position.y - draggedObject.transform.position.y) <= 0.8f)
        {
            this.transform.position = new Vector3(draggedObject.transform.position.x, 
                                                       draggedObject.transform.position.y,
                                                       draggedObject.transform.position.z);
            finish = true;
        }
        else
        {
            this.transform.position = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }
}
