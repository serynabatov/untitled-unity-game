using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Handles the play button on click event.
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Difficulty);
    }

    /// <summary>
    /// Handles the high score button on click event.
    /// </summary>
    public void HandleHighScoreButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.HighScore);
    }

    /// <summary>
    /// Handles the quit button on click event.
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }
}
