using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private bool _isCursorShown;


    private void Start()
    {
        HideCursor();

        if (_isCursorShown)
        {
            ShowCursor();
        }

        DialogueManager.DialogueStarted += ShowCursor;

        DialogueManager.DialogueEnded += HideCursor;
    }

    private void OnDestroy()
    {
        DialogueManager.DialogueStarted -= ShowCursor;

        DialogueManager.DialogueEnded -= HideCursor;
    }

    public static void HideCursor()
    {
        print("Cursor is hidden");
        Cursor.visible = false;
    }

    public static void ShowCursor()
    {
        print("Cursor is shown");
        Cursor.visible = true;
    }
}
