using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Singleton class for saveload local leaderboard
public class GameSaveLoad : MonoBehaviour
{
    public static GameSaveLoad instance;
    private string fileName = "SaveData.txt";
    private string filePath;
    private Dictionary<string, string> data;

    public readonly string MUSIC_KEY = "Music";
    public readonly string SFX_KEY = "SFX";
    public readonly string SOUND_KEY = "Sound";
    public readonly string SCORE_KEY = "Score";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath+ "/" + fileName;

        LoadData();
        //Directory.CreateDirectory(filePath);
    }
    
    public bool CheckFileExist()
    {
        return File.Exists(filePath);
    }

    public void Save()
    {
        string fileData = "";
        foreach(var item in data)
        {
            fileData += item.Key + "," + item.Value + "\n";
        }
        Debug.Log("Game Saved ");
        File.WriteAllText(filePath, fileData);
    }
    public void LoadData()
    {
        if(CheckFileExist())
        {
            //load from File
            data = new Dictionary<string, string>();
            string[] fileContent = File.ReadAllLines(filePath);
            foreach (string line in fileContent)
            {
                string[] temp = line.Split(',');
                if (temp.Length == 2)
                    data.Add(temp[0], temp[1]);
            }
        }
        else
        {
            Debug.Log("No save file found , creating new");
            CreateNewSave();
            Save();
        }
        
    }
    public string GetValue(string key)
    {
        return data[key];
    }
    public void SetValue(string key,string value)
    {
        data[key] = value;
    }
    private void CreateNewSave()
    {
        data = new Dictionary<string, string>
        {
            {SCORE_KEY,"0" },
            {MUSIC_KEY,"-10"},
            {SFX_KEY ,"-10"},
            {SOUND_KEY ,"1"}
        };

    }
}
