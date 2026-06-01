using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static int score = 0;

    public static void AddScore()
    {
        score++;
        Debug.Log("Á¡¼ö: " + score);
    }
}
