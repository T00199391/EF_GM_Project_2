using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileHandler : MonoBehaviour
{
    public void AddTextToFile(string levelNumber,string levelName)
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData.csv"))
        {
            File.CreateText(Application.persistentDataPath + "/GameData.csv");
        }

        File.WriteAllText(Application.persistentDataPath + "/GameData.csv", levelNumber + "," + levelName + ",");
    }

    public string[] ReadTextFromFile()
    {
        StreamReader reader = null;
        string[] gameValues = null;
        if (File.Exists(Application.persistentDataPath + "/GameData.csv"))
        {
            reader = new StreamReader(File.OpenRead(Application.persistentDataPath + "/GameData.csv"));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                gameValues = line.Split(',');
            }
        }

        return gameValues;
    }
}
