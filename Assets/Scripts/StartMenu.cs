using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI BestScoreAndNameText;

    void Start()
    {
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadPlayerInfo();
            Debug.Log(GameManager.Instance.playerName);
            if (string.IsNullOrEmpty(GameManager.Instance.playerName))
            {
                BestScoreAndNameText.gameObject.SetActive(false);
            }
            else
            {
               
                SetBestScore();
            }
        }
        else
        {
            Debug.LogError("GameManager instance is null");
        }
    }
    public void StartGame()
    {
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {   
            //if (!GameManager.Instance.playerName.Equals(nameInputField.text))
            //{
            //    GameManager.Instance.playerName = nameInputField.text;
            //    Debug.Log("Welcome: " + GameManager.Instance.playerName);
            //    GameManager.Instance.SavePlayer();
            //}

            GameManager.Instance.currentPlayer = nameInputField.text;
            Debug.Log("Welcome: " + GameManager.Instance.playerName);

            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Enter your name");
        }

        


    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetBestScore()
    {
        BestScoreAndNameText.text = "Best Score: " + GameManager.Instance.playerName + ": " + GameManager.Instance.score;
    }

}
