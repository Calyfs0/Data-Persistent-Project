using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int score;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod()
    {
        if (Instance == null)
        {
            GameObject gameManager = new GameObject("GameManager");
            Instance = gameManager.AddComponent<GameManager>();
            DontDestroyOnLoad(gameManager);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayerInfo();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        Player player = new Player
        {
            playerName = playerName,
            score = score
        };

        string json = JsonUtility.ToJson(player);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Player Saved");
    }

    public void LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Player player = JsonUtility.FromJson<Player>(json);
            playerName = player.playerName;
            score = player.score;
        }
    }
    [Serializable]
    class Player
    {
        public string playerName;
        public int score;
    }
}
