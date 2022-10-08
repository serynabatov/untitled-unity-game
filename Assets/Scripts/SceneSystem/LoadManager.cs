using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadManager : MonoBehaviour
{

    public enum LoadActions { LOAD, DELETE };

    // Prefab
    [SerializeField]
    private GameObject loadTemplatePrefab;

    [SerializeField]
    private GameObject loadGameMenu;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject pauseBackground;

    private bool executed = false;

    private int saveFiles;

    void LoadOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.LoadGame(timestamp);
        pauseBackground.SetActive(false);
        ConfirmationManager.GetInstance().SetActive2(false);
        PauseManager.GetInstance().ResumeGame();
    }

    void DeleteOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.DeleteGame(timestamp);
        ConfirmationManager.GetInstance().SetActive2(false);

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
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("LoadButtonPrefab"))
            {
                Destroy(go);
            }

            List<FileData> filesData = DataPersistenceManager.Instance.GetFiles();

            foreach (FileData fileData in filesData)
            {
                GameObject tile = Instantiate(loadTemplatePrefab, new Vector3(1, 1, 1), Quaternion.identity);
                tile.transform.parent = content.transform;
                tile.gameObject.GetComponentInChildren<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = fileData.metaData.locationName;
                tile.gameObject.GetComponentInChildren<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].text = fileData.metaData.timeStamp;

                tile.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => confirmationWinndow(fileData.metaData.timeStamp, LoadActions.LOAD));
                tile.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => confirmationWinndow(fileData.metaData.timeStamp, LoadActions.DELETE));
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

    private void confirmationWinndow(string fileName, LoadActions action)
    {
        if (!ConfirmationManager.GetInstance().ActiveSelf2())
        {
            switch (action)
            {
                case LoadActions.LOAD:
                    LoadConfirmationWindow(fileName);
                    break;
                case LoadActions.DELETE:
                    DeleteConfirmationWindow(fileName);
                    break;
            }
        }
    }

    private void LoadConfirmationWindow(string fileName)
    {
        ConfirmationManager.GetInstance().SetActive2(true);
        ConfirmationManager.GetInstance().GetYesButton2().onClick.AddListener(() => LoadOnClick(fileName));
    }

    private void DeleteConfirmationWindow(string fileName)
    {
        ConfirmationManager.GetInstance().SetActive2(true);
        ConfirmationManager.GetInstance().GetYesButton2().onClick.AddListener(() => DeleteOnClick(fileName));
    }
}