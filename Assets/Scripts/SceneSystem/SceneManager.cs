using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Prefab
    [SerializeField]
    public GameObject loadPrefab;

    [SerializeField]
    private GameObject loadGameMenu;

    public bool executed = false;

    private int saveFiles;
    
    public void Update()
    {
       LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        if (loadGameMenu.activeSelf() && executed == false)
        {
            List<FileData> filesData = DataPersistenceManager.Instance.GetFiles();

            executed = true;
        }
        else if (!loadGameMenu.activeSelf())
        {
            executed = false;
        }
    }

}