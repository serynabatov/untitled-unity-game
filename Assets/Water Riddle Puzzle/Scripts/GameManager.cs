using UnityEngine;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using Ink.Parsed;


public class GameManager : MonoBehaviour
{
    public const string WaterLevelStatus = "Water level status";
    public const string LastWaterSceneStatus = "mainVarBossFinished";

    public GameObject clearButton;
    public GameObject gameOverNote;
    public GameObject restartButton;
    public GameObject stateText;
    public GameObject startButton;
    public GameObject clearText;
    public GameObject flow;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    private DialogueVariables dialogueVariables;

    public bool gameRunning;
    public bool gameOver;
    public bool gameClear;

    private void Awake()
    {
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }
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
                WaterPuzzleSolved();
                SceneSystem.GetInstance().LoadThisLevel("Riddle 2"); // Должно быть Riddle 2
                break;
            case "Riddle 2":
                SceneSystem.GetInstance().LoadThisLevel("Riddle 3");
                break;
            case "Riddle 3":
                //WaterPuzzleSolved();
                SceneSystem.GetInstance().LoadThisLevel("Gameplay");
                break;
            case "ExampleRiddle":
                LastLevelSolved();
                SceneSystem.GetInstance().LoadThisLevel("Gameplay");
                break;
            default:
                Debug.LogWarning("Something gone wrong with water riddle scene names " + SceneManager.GetActiveScene().name);
                break;
        }
    }

    private void WaterPuzzleSolved()
    {
        PlayerPrefs.SetInt(WaterLevelStatus, 1);

        if (dialogueVariables.variables.ContainsKey("mainVarWaterFinished"))
        {
            dialogueVariables.variables["mainVarWaterFinished"] = Value.Create(true);
            dialogueVariables.SaveVariables();
            //dialogueVariables.variables.Add("mainVarWaterFinished", true);
        }
        if (PlayerPrefs.GetInt(WaterLevelStatus) == PlayerPrefs.GetInt("Crate level status"))
        {
            dialogueVariables.variables["cantalktoBoss"] = Value.Create(true);
            dialogueVariables.SaveVariables();
        }
    }

    private void LastLevelSolved()
    {
        PlayerPrefs.SetInt(LastWaterSceneStatus, 1);

        if (dialogueVariables.variables.ContainsKey("mainVarBossFinished"))
        {
            dialogueVariables.variables["mainVarBossFinished"] = Value.Create(true);
            dialogueVariables.SaveVariables();
        }      
    }
}
