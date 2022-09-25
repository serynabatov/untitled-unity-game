using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    // Prefab
    [SerializeField]
    public GameObject loadPrefab;

    [SerializeField]
    private GameObject loadGameMenu;

    private bool executed = false;

    private int saveFiles;

    public void Update()
    {
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        if (loadGameMenu.activeSelf && executed == false)
        {
            Debug.Log("HERE");
            List<FileData> filesData = DataPersistenceManager.Instance.GetFiles();

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            Debug.Log("DASAA");
            executed = false;
        }
    }

}