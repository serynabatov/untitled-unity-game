using System;
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
    [SerializeField] private bool useEncryption;
    [SerializeField] private bool useDebug;

    private FileData fileData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private const string patternTimeStamp = "dddd, dd MMMM yyyy HH:mm:ss";
    private const string patternFileName = "yyyy-MM-dd-HH-mm-ss-ffff";


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one!");
        }
        Instance = this;
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.dataPath + "/saves", useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (useDebug)
        {
            NewGame();
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
    public void LoadGame(string timestamp)
    {
        this.fileData = this.dataHandler.Load(timestamp);
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
        string fileName = Utilities.NamingFile(fileData.metaData.timeStamp, patternTimeStamp, patternFileName);
        // save the data
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref this.fileData.playerData);
        }

        fileData.metaData.ChangeTimestamp();

        this.dataHandler.Save(fileData, fileName);
    }

    public void DeleteGame(string fileName)
    {
        this.dataHandler.Delete(fileName);
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

    public List<FileData> GetFiles()
    {
        List<FileData> fileDatas = this.dataHandler.GetFiles();
        fileDatas.Sort((n1, n2) => n2.metaData.timeStamp.CompareTo(n1.metaData.timeStamp));
        return fileDatas;
    }
}