using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float trucx = 4;
    float trucy = 4;
    float speed = 9;
    float jumpForce = 9;

    Rigidbody2D rb;
    Animator animator;

    bool jumpInput;
    bool isAttacking = false;
    int jumpCount = 0;
    int maxJumpCount = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount && !isAttacking)
        {
            jumpInput = true;
        }

        // Tấn công
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");

            // Nếu bạn không dùng Animation Event thì dùng dòng dưới:
            Invoke("EndAttack", 0.5f); // Thời gian = độ dài clip Attack
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0f;

        // Chặn di chuyển khi đang attack
        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector3(-trucx, trucy, 1);
                horizontal = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector3(trucx, trucy, 1);
                horizontal = 1f;
            }
        }

        // Di chuyển
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Nhảy
        if (jumpInput)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reset Y trước khi nhảy
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
            jumpInput = false;
        }

        // Reset jump nếu đang chạm đất (đơn giản bằng kiểm tra tốc độ)
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && jumpCount > 0)
        {
            jumpCount = 0;
        }

        // Animation
        animator.SetBool("IsRunning", Mathf.Abs(rb.linearVelocity.x) > 0.01f && !isAttacking);
        animator.SetBool("IsIdling", Mathf.Abs(rb.linearVelocity.x) < 0.01f && jumpCount == 0 && !isAttacking);
        animator.SetBool("IsJumping", jumpCount > 0);
    }

    // Gọi từ Animation Event hoặc Invoke để kết thúc tấn công
    public void EndAttack()
    {
        isAttacking = false;
        // Debug.Log("Kết thúc tấn công");
    }
}
