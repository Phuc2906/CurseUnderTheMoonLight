using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    private GameObject currentEnemy; // Enemy đang trong phạm vi tấn công
    private bool isAttacking = false;

    void Update()
    {
        // Nhấn phím Space để tấn công
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack"); // Kích hoạt animation Attack
            Debug.Log("Player bắt đầu tấn công! isAttacking: " + isAttacking);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Lưu enemy trong phạm vi va chạm
        if (collision.collider.CompareTag("Enemy"))
        {
            currentEnemy = collision.collider.gameObject;
            Debug.Log("Player đang va chạm với Enemy: " + currentEnemy.name + ", Tag: " + collision.collider.tag);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Xóa tham chiếu đến enemy khi rời va chạm
        if (collision.collider.CompareTag("Enemy"))
        {
            currentEnemy = null;
            Debug.Log("Player không còn va chạm với Enemy!");
        }
    }

    // Hàm này được gọi từ Animation Event trong animation Attack
    public void HitEnemy()
    {
        Debug.Log("HitEnemy được gọi! currentEnemy: " + (currentEnemy != null ? currentEnemy.name : "null"));

        if (currentEnemy != null)
        {
            GameObject enemyToDestroy = currentEnemy; // lưu tạm
            currentEnemy = null; // reset trước khi destroy để tránh dùng lại

            Destroy(enemyToDestroy);
            Debug.Log("Enemy đã bị phá hủy!");
        }
        else
        {
            Debug.LogWarning("Không có Enemy để tấn công! currentEnemy is null.");
        }

        // Reset trạng thái tấn công sau khi animation hoàn tất
        isAttacking = false;
        animator.ResetTrigger("Attack");
        Debug.Log("Reset Attack, isAttacking: " + isAttacking);
    }
}