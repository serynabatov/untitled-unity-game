using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonWindow : MonoBehaviour
{
    public Transform box;
    public void OnEnable()
    {

        //box.localPosition = new Vector2(0, -Screen.height);
        box.localPosition = new Vector2(-Screen.width + 789, 0);
        //box.LeanMoveLocalY(0, 0.5f).setEaseInOutExpo().delay = 0.1f;
        box.LeanMoveLocalX(18, 0.5f).setEaseInOutExpo().delay = 0.1f;
        //box.LeanScale(Vector2.one, 0.8f);
    }
    public void CloseDialog()
    {
        //box.LeanMoveLocalY(-Screen.height, 1f).setEaseInOutExpo().setOnComplete(Complete);
        box.LeanMoveLocalX(-Screen.width + 789, 1f).setEaseInOutExpo().setOnComplete(Complete);
    }

    public void Complete()
    {
        gameObject.SetActive(false);
    }
}
