using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private bool _isCursorShown;

    private void Start()
    {
        if (_isCursorShown)
        {
            ShowCursor();
        }

        DialogueManager.DialogueStarted += ShowCursor;

        DialogueManager.DialogueEnded += HideCursor;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
    }
}
