/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pause and unpause game. Listens OnClick events
/// for the pause menu buttons
/// </summary>
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // pause the game when added to the screne
        Time.timeScale = 0;
    }

    /// <summary>
    /// Handles the resume button on click event.
    /// </summary>
    public void HandleResumeButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);

        // unpause game and destroy menu
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles the quit button on click event.
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);

        // unpause game, destroy menu and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
*/