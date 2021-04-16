using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileHandler : MonoBehaviour
{
    string lvName = "Level1";
    int lvNum = 1;

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData.csv"))
        {
            File.CreateText(Application.persistentDataPath + "/GameData.csv");
        }
    }

    private void SetLvName(string name)
    {
        lvName = name;
    }

    private void SetLvNum(int num)
    {
        lvNum = num;
    }

    public string GetLvName()
    {
        return lvName;
    }

    public int GetLvNum()
    {
        return lvNum;
    }

    public void AddTextToFile(string levelNumber,string levelName)
    {
        File.WriteAllText(Application.persistentDataPath + "/GameData.csv", levelNumber + "," + levelName + ",");
    }

    public void ReadTextFromFile()
    {
        StreamReader reader = null;
        string[] gameValues = null;
        try
        {
            if (File.Exists(Application.persistentDataPath + "/GameData.csv"))
            {
                reader = new StreamReader(File.OpenRead(Application.persistentDataPath + "/GameData.csv"));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    gameValues = line.Split(',');
                }

                SetLvName(gameValues[0]);
                SetLvNum((int)System.Char.GetNumericValue(gameValues[1][0]));
            }
        }catch(Exception e){
            Debug.Log(e);
        }
    }
}
