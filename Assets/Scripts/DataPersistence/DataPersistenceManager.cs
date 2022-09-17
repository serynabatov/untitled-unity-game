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

    /// <summary>
    /// The player data to track.
    /// </summary>
    private PlayerData playerData;

    private MetaData metaData;

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
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
        this.playerData = new PlayerData();
        this.metaData = new MetaData();
    }

    /// <summary>
    /// Loads the game.
    /// </summary>
    public void LoadGame()
    {
        this.playerData = dataHandler.Load();
        // this.metaData = dataHandler.Load();
        // if no data can be loaded, initialize a new game
        if (this.playerData == null)
        {
            Debug.Log("No Data was found");
            NewGame();
        }

        // load the data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(this.playerData);
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
            dataPersistenceObj.SaveData(ref this.playerData);
        }

        this.dataHandler.Save(playerData);
        // this.dataHandler.Save(metaData);
    }

    public void OnApplicationQuit()
    {
        //SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersitenceObjectsLocal = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersitenceObjectsLocal);
    }
}