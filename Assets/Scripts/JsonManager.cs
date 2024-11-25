using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public delegate void OnPlayerDataUpdated(string filePath);
    public static OnPlayerDataUpdated onPlayerDataUpdated;
    void Awake()
    {
        //filePath = Path.Combine(Application.dataPath, "PlayerData", "LeaderBoard.json");
    }
    public static void AddPlayer(string filePath, Player player)
    {
        // Player temp = new Player();
        // temp.PlayerName = _name;
        // temp.score = _sore;

        PlayerData currentPlayerData = GetPlayerData(filePath);
        currentPlayerData.playerData.Add(player);
        UpdatePlayerData(filePath, currentPlayerData);
    }
    public static PlayerData GetPlayerData(string _filePath)
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogError("File not found at: " + _filePath);
            return new PlayerData(); // Return an empty PlayerData if file doesn't exist
        }

        string json = File.ReadAllText(_filePath);
        PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);

        if (data == null)
        {
            data = new PlayerData(); // If deserialization fails, return an empty PlayerData
        }

        return data;
    }
    public static void UpdatePlayerData(string _filePath, PlayerData playerData)
    {
        // Use JsonConvert for serialization consistency
        string json = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        File.WriteAllText(_filePath, json);
        onPlayerDataUpdated?.Invoke(_filePath);
    }
}
