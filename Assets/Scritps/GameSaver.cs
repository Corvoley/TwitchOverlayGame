using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System;

public class SavePlayerData
{
    public List<Player> PlayerList = new List<Player>();

}

public class GameSaver : MonoBehaviour
{
    public static event Action OnGameStart;
    private string SaveGameFilePath => $"{Application.persistentDataPath}/Players.json";

    public static SavePlayerData CurrentPlayerSave { get; private set; }


    private bool IsLoaded => CurrentPlayerSave != null;
    private void SaveGameDataToFile(string filePath, SavePlayerData data)
    {

        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
        }
    }

    private SavePlayerData LoadGameDataFromFile(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<SavePlayerData>(jsonReader);
        }
    }

    public void SaveGame(SavePlayerData saveData)
    {
        CurrentPlayerSave = saveData;
        SaveGameDataToFile(SaveGameFilePath, saveData);
        Debug.Log(SaveGameFilePath);
    }

    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }

        CurrentPlayerSave = LoadGameDataFromFile(SaveGameFilePath) ?? new SavePlayerData();
        OnGameStart?.Invoke();

    }
    public void ForceLoadGame()
    {
        CurrentPlayerSave = LoadGameDataFromFile(SaveGameFilePath) ?? new SavePlayerData();
    }

}