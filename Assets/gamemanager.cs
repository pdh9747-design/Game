using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class gamemanager : MonoBehaviour
{
    public static gamemanager Instance;
    public static int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    
    public TextMeshProUGUI failText;

    [Header("Timer")]
    public float timeLimit = 90f;
    private float currentTime;

    // 아이템 개수가 계속 변하므로 List를 사용
    [Header("Items")]
    public List<GameObject> items = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        currentTime = timeLimit;

        
        if (failText != null)
            failText.gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        
        currentTime -= Time.deltaTime;

        if (timeText != null)
            timeText.text = "Time: " + Mathf.Ceil(currentTime);

        
        if (items.Count == 0)
        {
            GameClear();
            return;
        }

        
        if (currentTime <= 0)
        {
            GameOver();
        }
    }

    
    void GameClear()
    {
        Debug.Log("CLEAR!");

        if (timeText != null)
        {
            timeText.text = "CLEAR!";
            timeText.color = Color.green;
        }

        Time.timeScale = 0f;
    }

    
    void GameOver()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void ShowFallMessage()
    {
        if (failText == null) return;

        failText.gameObject.SetActive(true);
        failText.text = "ㅋㅋ";

        CancelInvoke(nameof(HideFallMessage));
        Invoke(nameof(HideFallMessage), 3f);
    }

    void HideFallMessage()
    {
        if (failText != null)
            failText.gameObject.SetActive(false);
    }
}