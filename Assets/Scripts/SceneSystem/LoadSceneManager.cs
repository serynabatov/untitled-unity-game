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

            foreach (FileData fileData in filesData)
            {
                GameObject tile = Instantiate(loadPrefab, new Vector3(1, 1, 1), Quaternion.identity);
                tile.transform.parent = parent.transform;
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

}