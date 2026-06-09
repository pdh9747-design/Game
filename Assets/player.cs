using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

    private Rigidbody rb;
    private Renderer rend;

    
    private Queue<Transform> pathQueue = new Queue<Transform>();
    private bool isPathInitialized = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<Renderer>();

        rend.material = new Material(rend.material);
        SetColor(Color.magenta);

        InitializePath();
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

        Debug.DrawRay(transform.position, Vector3.down * checkDistance, Color.red);

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
            gamemanager.Instance.ShowFallMessage();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
        }
    }

    void CheckDistance()
    {
        if (monster == null) return;

        float sqrDist = (monster.position - transform.position).sqrMagnitude;

        if (sqrDist < detectRange * detectRange)
        {
            Debug.Log("장애물 가까움!");
        }
    }

    void CheckFOV()
    {
        if (monster == null) return;

        Vector3 dirToMonster = (monster.position - transform.position).normalized;

        float dot = Vector3.Dot(transform.forward, dirToMonster);

        if (dot > 0.5f)
        {
            Debug.Log("시야 안에 장애물 있음!");
        }
    }

   
    void InitializePath()
    {
        if (nodes == null || nodes.Length == 0) return;

        foreach (Transform node in nodes)
        {
            pathQueue.Enqueue(node);
        }

        isPathInitialized = true;
    }

    void MoveAlongNodes()
    {
        if (!isPathInitialized || pathQueue.Count == 0) return;

        Transform target = pathQueue.Peek();

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveToNodeSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            pathQueue.Dequeue();

            if (pathQueue.Count == 0)
            {
                InitializePath();
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