using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagerThird : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Enter = " + collision.gameObject.name + " Game object = " + this.name);
        switch (collision.name)
        {
            case "BaseBox":
                ThirdLevelManager.GetInstance().BaseBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                ThirdLevelManager.GetInstance().IceBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                ThirdLevelManager.GetInstance().FireBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "Player":
                if (this.name == "Third")
                {
                    Application.Quit();
                    Debug.Log("Quit Scene");
                }
                break;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Trigger Exit = " + collision.gameObject.name + " Game object = " + this.name);
        switch (collision.name)
        {
            case "BaseBox":
                ThirdLevelManager.GetInstance().BaseBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                ThirdLevelManager.GetInstance().IceBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                ThirdLevelManager.GetInstance().FireBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
        }
    }
}
