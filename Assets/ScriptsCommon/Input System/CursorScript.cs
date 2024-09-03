using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private void Start()
    {
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
