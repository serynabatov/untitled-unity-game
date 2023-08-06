using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private bool useEncryption = false;
    private readonly string keyCodeWord = "Mechrodzh";

    public FileDataHandler(string dataDirPath, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.useEncryption = useEncryption;
    }

    private FileData LoadBean(string file)
    {
        FileData loadedData = null;
        string fullPath = Path.Combine(dataDirPath, file);
        try
        {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            if (useEncryption)
            {
                dataToLoad = EncryptDecrypt(dataToLoad);
            }

            loadedData = JsonUtility.FromJson<FileData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError("Error created while creating file " + fullPath);
        }

        return loadedData;
    }

    public FileData Load(string timestamp)
    {
        string[] files = Directory.GetFiles(dataDirPath);
        FileData loadedData = null;

        foreach (string file in files)
        {
            if (!file.EndsWith(".meta"))
            {
                loadedData = LoadBean(file);
                if (loadedData.metaData.timeStamp == timestamp)
                {
                    return loadedData;
                }
                loadedData = null;
            }
        }

        return loadedData;
    }

    public void Save(FileData data, string dataFileName)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize into JSON string
            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError("Error created while creating file " + fullPath);
        }
    }

    public void Delete(string timestamp)
    {
        string[] files = Directory.GetFiles(dataDirPath);
        FileData loadedData = null;

        foreach (string file in files)
        {
            if (!file.EndsWith(".meta"))
            {
                loadedData = LoadBean(file);
                if (loadedData.metaData.timeStamp == timestamp)
                {
                    File.Delete(Path.Combine(dataDirPath, file));
                    File.Delete(Path.Combine(dataDirPath, file + ".meta"));
                    return;
                }
                loadedData = null;
            }
        }

        Debug.LogError("Error there is no such a file " + timestamp);
    }

    public List<FileData> GetFiles()
    {
        List<FileData> fileDatas = new List<FileData>();

        string[] files = Directory.GetFiles(dataDirPath);

        foreach (string file in files)
        {
            if (!file.EndsWith(".meta"))
            {
                fileDatas.Add(LoadByName(file));
            }
        }

        return fileDatas;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ keyCodeWord[i % keyCodeWord.Length]);
        }

        return modifiedData;
    }

    private FileData LoadByName(string dataFileName)
    {
        FileData loadedData = null;

        if (File.Exists(Path.Combine(dataDirPath, dataFileName)))
        {
            loadedData = LoadBean(dataFileName);
        }
        return loadedData;
    }
}