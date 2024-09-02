using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagerFirst : MonoBehaviour
{
    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Trigger Enter = " + collision.gameObject.name + " Game object = " + this.name);
        switch (collision.name)
        {
            case "BaseBox":
                FirstLevelManager.GetInstance().BaseBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                _broker.Publish<int>((int)AudioClipName.IceBox);
                FirstLevelManager.GetInstance().IceBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                _broker.Publish<int>((int)AudioClipName.FireBox);
                FirstLevelManager.GetInstance().FireBox(true, this.gameObject, collision.GetComponent<Animator>());
                break;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "BaseBox":
                FirstLevelManager.GetInstance().BaseBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "IceBox":
                FirstLevelManager.GetInstance().IceBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
            case "FireBox":
                FirstLevelManager.GetInstance().FireBox(false, this.gameObject, collision.GetComponent<Animator>());
                break;
        }
    }
}
