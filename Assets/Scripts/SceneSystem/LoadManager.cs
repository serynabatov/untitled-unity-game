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

    [SerializeField]
    private GameObject сonfirmationLoadWindow;

    private bool executed = false;

    private int saveFiles;

    void LoadOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.LoadGame(timestamp);
        pauseBackground.SetActive(false);
        ConfirmationManager.GetInstance().SetActive(сonfirmationLoadWindow, false);
        PauseManager.GetInstance().ResumeGame();
    }

    void DeleteOnClick(string timestamp, GameObject gameObject)
    {
        DataPersistenceManager.Instance.DeleteGame(timestamp);
        ConfirmationManager.GetInstance().SetActive(сonfirmationLoadWindow, false);
        Destroy(gameObject);
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

                tile.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => confirmationWinndow(fileData.metaData.timeStamp, LoadActions.LOAD, null));
                tile.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => confirmationWinndow(fileData.metaData.timeStamp, LoadActions.DELETE, tile.gameObject));
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

    private void confirmationWinndow(string fileName, LoadActions action, GameObject gameObject)
    {
        if (!ConfirmationManager.GetInstance().ActiveSelf(сonfirmationLoadWindow))
        {
            switch (action)
            {
                case LoadActions.LOAD:
                    LoadConfirmationWindow(fileName);
                    break;
                case LoadActions.DELETE:
                    DeleteConfirmationWindow(fileName, gameObject);
                    break;
            }
        }
    }

    private void LoadConfirmationWindow(string fileName)
    {
        ConfirmationManager.GetInstance().SetActive(сonfirmationLoadWindow, true);
        ConfirmationManager.GetInstance().GetYesButton(сonfirmationLoadWindow).onClick.AddListener(() => LoadOnClick(fileName));
    }

    private void DeleteConfirmationWindow(string fileName, GameObject gameObject)
    {
        ConfirmationManager.GetInstance().SetActive(сonfirmationLoadWindow, true);
        ConfirmationManager.GetInstance().GetYesButton(сonfirmationLoadWindow).onClick.AddListener(() => DeleteOnClick(fileName, gameObject));
    }

    /// <summary>
    /// Окно отказа от загрузки сейва
    /// </summary>
    public void DismissLoadGame()
    {
        ConfirmationManager.GetInstance().ExecuteNoButton(сonfirmationLoadWindow);
    }
}