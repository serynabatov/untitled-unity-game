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

    private FileData LoadBean(string file, string timestamp)
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
            Debug.LogError("Error created while creating file " + fullPath);
        }

        if (loadedData.metaData.timeStamp == timestamp)
        {
            return loadedData;
        }
        else
        {
            return null;
        }
    }

    public FileData Load(string dataFileName)
    {
        string[] files = Directory.GetFiles(dataDirPath);
        FileData loadedData = null;

        foreach (string file in files)
        {
            loadedData = LoadBean(file, dataFileName);
            if (loadedData != null)
            {
                break;
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
            Debug.LogError("Error created while creating file " + fullPath);
        }
    }

    public void Delete(string timestamp)
    {
        string fullPath = Path.Combine(dataDirPath, timestamp);

        try
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                File.Delete(fullPath + ".meta");
            }
            else
            {
                Debug.LogError("Error there is no such a file " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in opening the file " + fullPath);
        }
    }

    public List<FileData> GetFiles()
    {
        List<FileData> fileDatas = new List<FileData>();

        string[] files = Directory.GetFiles(dataDirPath);

        foreach (string file in files)
        {
            if (!file.EndsWith(".meta"))
            {
                fileDatas.Add(Load(file));
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
}