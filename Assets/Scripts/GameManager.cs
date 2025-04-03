using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string currentPlayer;
    public string playerName;
    public int score;
    string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayerInfo();
        path = Application.persistentDataPath + "/savefile.json";
    }

    public void SavePlayer()
    {
        Player player = new Player
        {
            playerName = playerName,
            score = score
        };
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        string json = JsonUtility.ToJson(player);
        File.WriteAllText(path, json);
        Debug.Log("Player Saved");
    }

    public void LoadPlayerInfo()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            Player player = JsonUtility.FromJson<Player>(json);
            playerName = player.playerName;
            score = player.score;
        }
    }
    [Serializable]
    class Player
    {
        public string currentPlayer;
        public string playerName;
        public int score;
    }
}
