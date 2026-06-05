using UnityEngine;

public class item : MonoBehaviour
{
    private void Start()
    {
        gamemanager.Instance.items.Add(gameObject);
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        gamemanager.score++;

        gamemanager.Instance.items.Remove(gameObject);

        Destroy(gameObject);
    }
}