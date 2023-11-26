using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetInstance().GetParryPressed())
        {
            LoadThisLevel("Crate Puzzle");
        }
    }

    public void LoadThisLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}
