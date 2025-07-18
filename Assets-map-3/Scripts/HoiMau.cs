using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log("Đã hồi máu cho Player!");
                Destroy(gameObject);
            }
        }
    }
}
