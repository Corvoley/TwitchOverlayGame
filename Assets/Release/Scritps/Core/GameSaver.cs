using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System;

public class PlayerData
{
    public List<Player> PlayerList = new List<Player>();
}

public class GameSaver : MonoBehaviour
{
    public static event Action OnGameStart;
    private string SavePlayerFilePath => $"D:/Unity/TwitchProject/Players.json";
    public static PlayerData CurrentPlayerSave { get; private set; }
    private bool IsLoaded => CurrentPlayerSave != null;

    private void SaveGameDataToFile(string filePath, System.Object data)
    {

        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
           
        }
    }
    private T LoadGameDataFromFile<T>(string filePath) where T : new()
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<T>(jsonReader);
        }
    }
    public void SavePlayerData(PlayerData saveData)
    {
        CurrentPlayerSave = saveData;
        SaveGameDataToFile(SavePlayerFilePath, saveData);       
    }
    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }

        CurrentPlayerSave = LoadGameDataFromFile<PlayerData>(SavePlayerFilePath) ?? new PlayerData();    
        OnGameStart?.Invoke();

    }
    public void ForceLoadGame()
    {
        //CurrentPlayerSave = LoadGameDataFromFile(SavePlayerFilePath) ?? new SavePlayerData();
    }

}
