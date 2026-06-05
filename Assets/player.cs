using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float checkDistance = 2.5f;

    [Header("Monster / Detection")]
    public Transform monster;
    public float detectRange = 5f;

    [Header("Path Nodes")]
    public Transform[] nodes;
    public float moveToNodeSpeed = 3f;
    private int currentNodeIndex = 0;

    private Rigidbody rb;
    private Renderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<Renderer>();

        rend.material = new Material(rend.material);
        SetColor(Color.magenta);
    }

    void Update()
    {
        Move();
        Jump();
        CheckFall();

        LookAtMonster();
        CheckDistance();
        CheckFOV();

        MoveAlongNodes();
    }

    
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

    
    void Jump()
    {
        bool isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            checkDistance
        );

        Debug.DrawRay(
            transform.position,
            Vector3.down * checkDistance,
            Color.red
        );

        if (isGrounded &&
            Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    
    void CheckFall()
    {
        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
    void LookAtMonster()
    {
        if (monster == null) return;

        Vector3 dir = monster.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                5f * Time.deltaTime
            );
        }
    }

  
    void CheckDistance()
    {
        if (monster == null) return;

        Vector3 diff = monster.position - transform.position;
        float sqrDist = diff.sqrMagnitude;

        if (sqrDist < detectRange * detectRange)
        {
            Debug.Log("장애물 가까움!");
        }
    }

   
    void CheckFOV()
    {
        if (monster == null) return;

        Vector3 dirToMonster = monster.position - transform.position;
        dirToMonster.y = 0;

        float angle = Vector3.Angle(transform.forward, dirToMonster);

        float fov = 60f;

        if (angle < fov * 0.5f)
        {
            Debug.Log("시야 안에 몬스터 있음!");
        }
    }

    
    void MoveAlongNodes()
    {
        if (nodes == null || nodes.Length == 0) return;

        Transform targetNode = nodes[currentNodeIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetNode.position,
            moveToNodeSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
        {
            currentNodeIndex++;

            if (currentNodeIndex >= nodes.Length)
            {
                currentNodeIndex = 0;
            }
        }
    }

  
    void SetColor(Color color)
    {
        if (rend == null) return;

        if (rend.material.HasProperty("_BaseColor"))
            rend.material.SetColor("_BaseColor", color);
        else
            rend.material.color = color;
    }
}