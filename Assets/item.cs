using UnityEngine;

public class item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            gamemanager.score++;   // ⭐ 점수 증가 (여기!)
            Debug.Log("아이템 획득! 점수: " + gamemanager.score);

            Destroy(gameObject);
        }
    }
}
