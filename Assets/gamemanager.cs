using UnityEngine;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}