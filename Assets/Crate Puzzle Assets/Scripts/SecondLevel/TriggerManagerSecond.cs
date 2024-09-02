using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagerSecond : MonoBehaviour
{
    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Enter = " + collision.gameObject.name + " Game object = " + this.name);
        switch (collision.name)
        {
            case "BaseBox":
                SecondLevelManager.GetInstance().BaseBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                _broker.Publish<int>((int)AudioClipName.IceBox);
                SecondLevelManager.GetInstance().IceBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                _broker.Publish<int>((int)AudioClipName.FireBox);
                SecondLevelManager.GetInstance().FireBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Trigger Exit = " + collision.gameObject.name + " Game object = " + this.name);
        switch (collision.name)
        {
            case "BaseBox":
                SecondLevelManager.GetInstance().BaseBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                SecondLevelManager.GetInstance().IceBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                SecondLevelManager.GetInstance().FireBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
        }
    }
}
