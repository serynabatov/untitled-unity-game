using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystemManager 
{
    private readonly static string dataPath = Application.dataPath + "/saves/";
    public static void Init()
    {
       if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
    }

    public static void Save(string saveData, string dataFile)
    {
        File.WriteAllText(dataPath + dataFile, saveData);
    }

    public static string Load(string dataFile)
    {
        if (File.Exists(dataPath + dataFile))
        {
            return File.ReadAllText(dataPath + dataFile);
        }
        else
        {
            // TODO: Нужно узнать хардкод JSON в С#
            return null;
        }
    }

}
