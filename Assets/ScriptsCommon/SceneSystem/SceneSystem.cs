using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public static event Action OnSceneChange;

    private static SceneSystem instance;
    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Scene System in the scene");
        }
        instance = this;
    }

    public static SceneSystem GetInstance()
    {
        return instance;
    }

    public void LoadThisLevel(string levelName)
    {
        OnSceneChange?.Invoke();
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Crossfade_Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
