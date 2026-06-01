using UnityEngine;

public class item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("æ∆¿Ã≈€ »πµÊ!");
            Destroy(gameObject); 
        }
    }
}
