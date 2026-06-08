using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(2, 2.5f, -5f);
    public float followSpeed = 10f;

    void LateUpdate()
    {
        if (player == null) return;

       
        Vector3 targetPos = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * followSpeed
        );

        
        Vector3 lookTarget = player.position + Vector3.up * 1.2f;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookTarget - transform.position),
            Time.deltaTime * followSpeed
        );
    }

}
