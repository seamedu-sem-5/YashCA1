using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    string filePath;
    [SerializeField] GameObject scorePrefab;
    [SerializeField] GameObject spawnLocation;

    private List<ScoreDisplay> scoreList = new List<ScoreDisplay>();

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "LeaderBoard.json");
    }

    void Start()
    {
        if (!File.Exists(filePath))
        {
            // Create the file with an empty PlayerData if it doesn’t exist
            PlayerData emptyData = new PlayerData();
            JsonManager.UpdatePlayerData(filePath, emptyData);
        }
        DisplayLeaderBoard();
    }



    public void DisplayLeaderBoard()
    {
        //DebugFile();
        PlayerData currentPlayerData = SortPlayerData(JsonManager.GetPlayerData(filePath));
        //Debug.LogWarning("After Sort");
        //DebugPlayerData(currentPlayerData);
        for (int i = 0; i < currentPlayerData.playerData.Count; i++)
        {
            ScoreDisplay temp = Instantiate(scorePrefab).GetComponent<ScoreDisplay>();
            temp.transform.SetParent(spawnLocation.transform);
            temp.transform.localScale = Vector3.one;
            temp.SetScore(i + 1, currentPlayerData.playerData[i].PlayerName, int.Parse(currentPlayerData.playerData[i].score));
        }
    }
    PlayerData SortPlayerData(PlayerData playerData)
    {
        bool swapped;
        for (int i = 0; i < playerData.playerData.Count - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < playerData.playerData.Count - i - 1; j++)
            {
                if (int.Parse(playerData.playerData[j].score) < int.Parse(playerData.playerData[j + 1].score))
                {
                    Player temp = playerData.playerData[j];
                    playerData.playerData[j] = playerData.playerData[j + 1];
                    playerData.playerData[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
            {
                return playerData;
            }
        }
        return playerData;

    }
    public void DebugFile()
    {
        PlayerData currentPlayerData = JsonManager.GetPlayerData(filePath);
        if (currentPlayerData.playerData.Count > 0)
        {
            foreach (Player player in currentPlayerData.playerData)
            {
                Debug.Log(player.PlayerName + " : " + player.score);
                //int score = int.Parse(player.score);
            }
            Debug.Log("____________________________________________");
        }
        else
        {
            Debug.Log("File Empty");
        }
    }
    public void DebugPlayerData(PlayerData currentPlayerData)
    {
        if (currentPlayerData.playerData.Count > 0)
        {
            foreach (Player player in currentPlayerData.playerData)
            {
                Debug.Log(player.PlayerName + " : " + player.score);
                //int score = int.Parse(player.score);
            }
            Debug.Log("____________________________________________");
        }
        else
        {
            Debug.Log("File Empty");
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public List<Player> playerData = new List<Player>();
}

[System.Serializable]
public class Player
{
    public string PlayerName;
    public string score;

    public Player(string name, string _score)
    {
        PlayerName = name;
        score = _score;
    }
}
