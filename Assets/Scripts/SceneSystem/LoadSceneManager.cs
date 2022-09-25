using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    // Prefab
    [SerializeField]
    public GameObject loadPrefab;

    [SerializeField]
    public GameObject loadGameMenu;

    [SerializeField]
    public GameObject parent;

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
            List<FileData> filesData = DataPersistenceManager.Instance.GetFiles();

            for (FileData fileData : filesData)
            {
                GameObject tile = Instantiate(loadPrefab, new Vector2(1, 1, 1), Quaternon.Identity);
                tile.transform.position = parent.transform;
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

}