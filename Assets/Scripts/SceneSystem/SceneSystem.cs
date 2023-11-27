using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    private static SceneSystem instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static SceneSystem GetInstance()
    {
        return instance;
    }

    void Update()
    {
        if (InputManager.GetInstance().GetParryPressed())
        {
            //LoadThisLevel("Crate Puzzle");
        }
    }

    public void LoadThisLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    public void CheckSceneToStart()
    {
        if (DialogueManager.GetInstance().GetVariableState("mainVarCrate")) //! Написать здесь нужно название переменной по условию которой нужно запускать сцену
        {
            Debug.Log("CRATE PUZZLE START");
        }
        if (DialogueManager.GetInstance().GetVariableState("mainVarWate")) //! Написать здесь нужно название переменной по условию которой нужно запускать сцену
        {
            Debug.Log("WATER PUZZLE START");
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}
