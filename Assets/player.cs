using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;

    // ===== 점프 =====
    public float jumpForce = 5f;
    public float checkDistance = 0.6f;

    private Rigidbody rb;

    // ===== 몬스터 감지 =====
    public Transform monster;
    public float radiusA = 1f;
    public float radiusB = 1f;
    private bool detected = false;

    // ===== 방향 상태 =====
    private bool facingForward = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        DetectMonster();

        // ===== S키 회전 (핵심 추가 부분) =====
        if (Keyboard.current != null)
        {
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                facingForward = !facingForward;

                float yRotation = facingForward ? 0f : 180f;
                transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }
    }

    // ================= 이동 =================
    void Move()
    {
        Vector3 moveDir = Vector3.zero;

        if (Keyboard.current != null)
        {
            float h = 0;
            float v = 0;

            if (Keyboard.current.aKey.isPressed) h = -1;
            if (Keyboard.current.dKey.isPressed) h = 1;
            if (Keyboard.current.wKey.isPressed) v = 1;
            if (Keyboard.current.sKey.isPressed) v = -1;

            moveDir = new Vector3(h, 0, v).normalized;
        }

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    // ================= 점프 =================
    void Jump()
    {
        bool isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            checkDistance
        );

        Debug.DrawRay(
            transform.position + Vector3.up * 0.1f,
            Vector3.down * checkDistance,
            isGrounded ? Color.green : Color.red
        );

        if (isGrounded &&
            Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // ================= 몬스터 감지 =================
    bool IsOverlapping()
    {
        if (monster == null) return false;

        Vector3 diff = transform.position - monster.position;
        float distanceSq = diff.sqrMagnitude;

        float radiusSum = radiusA + radiusB;
        float radiusSumSq = radiusSum * radiusSum;

        return distanceSq <= radiusSumSq;
    }

    void DetectMonster()
    {
        if (IsOverlapping() && !detected)
        {
            detected = true;
            Debug.Log("몬스터가 플레이어를 감지!");
        }

        if (!IsOverlapping())
        {
            detected = false;
        }
    }
}