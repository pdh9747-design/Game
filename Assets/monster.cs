using UnityEngine;

public class monster : MonoBehaviour
{
    public Transform player;
    public float detectDistance = 5f;
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool hasJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null || rb == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectDistance)
        {
            // ================= 도망 =================
            Vector3 dir = (transform.position - player.position).normalized;
            dir.y = 0;

            rb.linearVelocity = new Vector3(
                dir.x * moveSpeed,
                rb.linearVelocity.y,
                dir.z * moveSpeed
            );

            // ================= 점프 1번 =================
            if (!hasJumped)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }
        }
        else
        {
            hasJumped = false; // 다시 초기화 (다음 감지 때 점프 가능)
        }
    }
}
