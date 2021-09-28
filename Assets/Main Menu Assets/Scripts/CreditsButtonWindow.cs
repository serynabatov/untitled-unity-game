using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonWindow : MonoBehaviour
{
    public Transform box;
    public void OnEnable()
    {

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseInOutExpo().delay = 0.1f;
    }
    public void CloseDialog()
    {
        box.LeanMoveLocalY(-Screen.height, 1f).setEaseInOutExpo().setOnComplete(Complete);
    }

    public void Complete()
    {
        gameObject.SetActive(false);
    }
}
