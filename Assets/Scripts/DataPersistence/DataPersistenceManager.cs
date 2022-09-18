using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Class <c>DataPersistenceManager</c> is a singleton manager
/// </summary>
public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Configuration")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [SerializeField] private bool useDebug;

    private FileData fileData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one!");
        }
        instance = this;
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.dataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (useDebug)
        {
            LoadGame();
        }
    }

    /// <summary>
    /// Initialize the new game
    /// </summary>
    public void NewGame()
    {
        this.fileData = new FileData();
    }

    /// <summary>
    /// Loads the game.
    /// </summary>
    public void LoadGame()
    {
        this.fileData = this.dataHandler.Load();
        // if no data can be loaded, initialize a new game
        if (this.fileData == null)
        {
            Debug.Log("No Data was found");
            NewGame();
        }

        // load the data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(this.fileData.playerData);
        }
    }

    /// <summary>
    /// Saves the game.
    /// </summary>
    public void SaveGame()
    {
        // save the data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref this.fileData.playerData);
        }

        this.dataHandler.Save(fileData);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersitenceObjectsLocal = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersitenceObjectsLocal);
    }
}