using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isAttacking = false;
    private float attackCooldown = 1.5f;
    private float lastAttackTime = -10f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Hướng nhìn (flipX đúng chiều)
        if (player.position.x < transform.position.x)
            spriteRenderer.flipX = false; // Mặt trái
        else
            spriteRenderer.flipX = true;  // Mặt phải

        // Nếu chưa đủ gần, di chuyển
        if (distance > attackRange)
        {
            isAttacking = false;
            animator.SetBool("isRunning", true);

            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            animator.ResetTrigger("Attack"); // Ngưng chém
        }
        else
        {
            animator.SetBool("isRunning", false);

            // Tấn công nếu hết cooldown
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }
}
