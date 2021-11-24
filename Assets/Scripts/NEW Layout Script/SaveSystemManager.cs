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

    public static void Save(string saveData)
    {
        File.WriteAllText(dataPath +"npcOneState.txt", saveData);
    }

    public static string Load()
    {
        if (File.Exists(dataPath + "npcOneState.txt"))
        {
            return File.ReadAllText(dataPath + "npcOneState.txt");
        }
        else
        {
            // TODO: Нужно узнать хардкод JSON в С#
            return null;
        }
    }

}
