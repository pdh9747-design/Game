using UnityEngine;

public class item : MonoBehaviour
{
    private void Start()
    {
        gamemanager.Instance.items.Add(gameObject);
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Unity 물리 이벤트 흐름 설명:
    // 1. Player와 Item 둘 다 Collider를 가지고 있어야 함
    // 2. Item 또는 Player 중 하나는 Trigger 활성화 필요
    // 3. 두 Collider가 겹치면 Unity 물리 엔진이 감지
    // 4. 충돌 순간 OnTriggerEnter 자동 호출됨
    // 5. 여기서 점수 증가 및 아이템 삭제 처리

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        gamemanager.score++;

        gamemanager.Instance.items.Remove(gameObject);

        Destroy(gameObject);
    }
}