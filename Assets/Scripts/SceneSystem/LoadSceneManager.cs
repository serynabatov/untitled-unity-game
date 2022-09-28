using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSceneManager : MonoBehaviour
{
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
        //TODO: сделать confirmation Window
        pauseBackground.SetActive(false);
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
            //TODO: Надо найти способ чтобы очистить content перед заполнением его сейвами, чтобы не было дубликатов
            Button[] buttonList = content.GetComponentsInChildren<Button>();
            if (buttonList.Length != 0)
            {
                foreach (Button button in buttonList)
                {
                    Destroy(button);
                }
            }

            List<FileData> filesData = DataPersistenceManager.Instance.GetFiles();

            foreach (FileData fileData in filesData)
            {
                GameObject tile = Instantiate(loadTemplatePrefab, new Vector3(1, 1, 1), Quaternion.identity);
                tile.transform.parent = content.transform;
                tile.gameObject.GetComponentInChildren<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text = fileData.metaData.locationName;
                tile.gameObject.GetComponentInChildren<Button>().gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].text = fileData.metaData.timeStamp;

                tile.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => LoadOnClick(fileData.metaData.timeStamp));
                tile.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => DeleteOnClick(fileData.metaData.timeStamp));
            }

            executed = true;
        }
        else if (!loadGameMenu.activeSelf)
        {
            executed = false;
        }
    }

}