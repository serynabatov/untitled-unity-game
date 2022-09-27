using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSceneManager : MonoBehaviour
{
    // Prefab
    [SerializeField]
    public GameObject loadPrefab;

    [SerializeField]
    public GameObject loadGameMenu;

    [SerializeField]
    public GameObject parent;

    public Button loadButton, deleteButton;
    public TextMeshProUGUI timeText;

    [SerializeField]
    public GameObject menu;

    private bool executed = false;

    private int saveFiles;

    void Start()
    {
        loadButton.onClick.AddListener(() => LoadOnClick(timeText.text));
        deleteButton.onClick.AddListener(() => DeleteOnClick(timeText.text));
    }

    void LoadOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.LoadGame(timestamp);
        //TODO: сделать confirmation Window
        menu.SetActive(false);
        PauseManager.GetInstance().ResumeGame();
    }

    void DeleteOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.DeleteGame(timestamp);

        executed = false;
    }

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

                //timeText.text = fileData.metaData.timeStamp;
                //tile.gameObject.GetComponent<TextMeshProUGUI>().text
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

}