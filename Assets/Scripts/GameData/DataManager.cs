using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour
{
    string extensionFile = ".gamedata";
    
    /*
     Forma tradicional
     
    public void SaveData(GameData gameData)
    {
        string filepath = $"{Application.persistentDataPath}/saves{extensionFile}";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create($"{filepath}");
        bf.Serialize(file, gameData);
        file.Close();
        Debug.Log($"Saved path: {filepath}");
    }

    public GameData LoadData()
    {
        string filepath = $"{Application.persistentDataPath}/saves{extensionFile}";
        GameData gameData = new GameData();

        if(File.Exists($"{filepath}"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);
            gameData = bf.Deserialize(file) as GameData;
            file.Close();
        }

        return gameData;
    }

    */


    // Json 
     public void SaveData(GameData gameData)
    {
        string filepath = $"{Application.persistentDataPath}/saves{extensionFile}";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filepath);

        string json = JsonUtility.ToJson(gameData);

        bf.Serialize(file, json);
        file.Close();
        Debug.Log($"Saved path: {filepath}");
    }

    public GameData LoadData()
    {
        string filepath = $"{Application.persistentDataPath}/saves{extensionFile}";
        GameData gameData = new GameData();

        if(File.Exists(filepath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);
            string json = bf.Deserialize(file) as string;
            gameData = JsonUtility.FromJson<GameData>(json);
            file.Close();
        }

        return gameData;
    }
}
