﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject clearButton;
    public GameObject gameOverNote;
    public GameObject restartButton;
    public GameObject stateText;
    public GameObject startButton;
    public GameObject clearText;
    public GameObject flow;

    public bool gameRunning;
    public bool gameOver;
    public bool gameClear;



    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverNote.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        if (gameClear)
        {
            clearText.gameObject.SetActive(true);
            clearButton.gameObject.SetActive(true);
        }
    }

    public void GameRestart()
    {
        SceneSystem.GetInstance().LoadThisLevel(SceneManager.GetActiveScene().name);
        //edge.gameObject.SetActive(true);
    }
    public void GameStart()
    {
        gameRunning = true;
        stateText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void LoadScene()
    {
        int index;
        index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void ToNextScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Riddle 1":
                SceneSystem.GetInstance().LoadThisLevel("Riddle 2");
                break;
            case "Riddle 2":
                SceneSystem.GetInstance().LoadThisLevel("Riddle 3");
                break;
            case "Riddle 3":
                SceneSystem.GetInstance().LoadThisLevel("Gameplay");
                break;
            default:
                Debug.LogWarning("Something gone wrong with water riddle scene names " + SceneManager.GetActiveScene().name);
                break;
        }
    }
}