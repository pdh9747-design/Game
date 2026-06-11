using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Transform node1;
    public Transform node2;

    public float moveSpeed = 3f;

    private Transform target;

    void Start()
    {
        target = node2;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            if (target == node2)
                target = node1;
            else
                target = node2;
        }
    }
}
