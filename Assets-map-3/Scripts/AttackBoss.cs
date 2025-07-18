using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    public Animator animator;
    public bool IsAttacking = false;

    private GameObject currentTarget; // Player đang bị lock

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !IsAttacking)
        {
            IsAttacking = true;
            animator.SetTrigger("Attack"); // Kích hoạt animation Attack
            Debug.Log("Bắt đầu tấn công Player!");

            currentTarget = collision.collider.gameObject;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Đảm bảo trạng thái Attack được duy trì
            if (!IsAttacking)
            {
                IsAttacking = true;
                animator.SetTrigger("Attack");
                Debug.Log("Tiếp tục tấn công Player!");
            }
            currentTarget = collision.collider.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            IsAttacking = false;
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack"); // Kích hoạt trigger để dừng Attack
            Debug.Log("Dừng tấn công Player!");

            currentTarget = null;
        }
    }

    // Hàm này được gọi từ Animation Event
    public void HitPlayer()
    {
        if (currentTarget != null)
        {
            Health hp = currentTarget.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(50f);
                Debug.Log("Player dính đòn tấn công!");
            }
            else
            {
                Debug.LogError("Không tìm thấy component Health trên Player!");
            }
        }
        else
        {
            Debug.LogWarning("Không có mục tiêu để tấn công!");
        }
    }
}