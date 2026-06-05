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

    [Header("Timer")]
    public float timeLimit = 60f;
    private float currentTime;

    [Header("Data Structure")]
    public List<GameObject> items = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        currentTime = timeLimit;
    }

    void Update()
    {
       
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        
        currentTime -= Time.deltaTime;

        if (timeText != null)
            timeText.text = "Time: " + Mathf.Ceil(currentTime);

       
        if (currentTime <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}