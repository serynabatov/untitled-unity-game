using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages navigation through the menu system
/// </summary>
public class MenuManager 
{
    public static void GoToMenu(MenuName name)
    {
        switch(name)
        {
            case MenuName.Difficulty:
                SceneManager.LoadScene("DifficultyMenu");
                break;
            case MenuName.HighScore:
                // deactivate menu and instantiate a prefab
                //GameObject mainMenuCanvas = GameObject.Find("MainMenuCanvas");
                //if (mainMenuCanvas != null)
                //{
                //    mainMenuCanvas.SetActive(false);
                //}
                //Object.Instantiate(Resources.Load("HighScoreMenu"));
                // TODO: implement HighScoreMenu
                // For now it's just a pass
                SceneManager.LoadScene("HighScoreMenu");
                break;
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Pause:
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;
        }
    }
}
