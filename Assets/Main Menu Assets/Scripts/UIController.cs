using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button exitButton;
    public Label label;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("startButton");
        optionsButton = root.Q<Button>("optionsButton");
        creditsButton = root.Q<Button>("creditsButton");
        exitButton = root.Q<Button>("exitButton");
        label = root.Q<Label>("label");

        startButton.clicked += StartButtonPressed;
        exitButton.clicked += ExitButtonPressed;
        optionsButton.clicked += OptionsButtonPressed;
    }

    private void StartButtonPressed()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void ExitButtonPressed()
    {
        Application.Quit();
    }

    private void OptionsButtonPressed()
    {
        label.text = "SSSSSSSS";
        label.style.display = DisplayStyle.None;
    }
}
