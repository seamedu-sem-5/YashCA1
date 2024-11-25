using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public string playerName;
    public static GameManager Instance { get; private set; }
    private string filePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
        filePath = Path.Combine(Application.persistentDataPath, "LeaderBoard.json");
    }
    public void GameOver(int _score)
    {
        score = _score;
        JsonManager.AddPlayer(filePath, new Player(playerName, score.ToString()));
        GetComponent<ChangeScene>().NextScene();
    }
    public void SetPlayerName(string _name)
    {
        playerName = _name;
    }
}
