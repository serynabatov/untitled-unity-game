using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseUIController : MonoBehaviour
{
    public Button continueButton;
    public Button saveButton;
    public Button optionsButton;
    public Button exitToMainmenuButton;
    public VisualElement pauseBackground;
    public VisualElement root;
    public DropdownMenu dropdownMenu;

    private void Awake()
    { /*
        root = GetComponent<UIDocument>().rootVisualElement;

        continueButton = root.Q<Button>("continueButton");
        saveButton = root.Q<Button>("saveButton");
        optionsButton = root.Q<Button>("optionsButton");
        exitToMainmenuButton = root.Q<Button>("exitToMainmenuButton");
        pauseBackground = root.Q<VisualElement>("BackGround");

        */
    }
    // Start is called before the first frame update
    void Start()
    {
       // continueButton.clickable.clicked += Continue;
        //exitToMainmenuButton.clickable.clicked += ExitToMainMenu;
    }

    private void Continue()
    {
        PauseManager.GetInstance().ResumeGame();
    }

    private void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
