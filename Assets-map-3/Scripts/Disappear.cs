using UnityEngine;

public class DestroyOnPlayerTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
    }
}
