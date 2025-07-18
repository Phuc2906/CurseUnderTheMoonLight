using UnityEngine;

public class KeyAxis : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool jumpInput;
    private bool canJump;

    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        originalScale = transform.localScale; // Lưu scale gốc
    }

    void Update()
    {
        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumpInput = true;
        }
    }

    void FixedUpdate()
    {
        // Kiểm tra có chạm đất không
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        // Di chuyển ngang
        float move = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) move = 1;

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Xoay mặt không thay đổi scale gốc
        if (move != 0)
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(move), originalScale.y, originalScale.z);

        // Nhảy
        if (jumpInput)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpInput = false;
            canJump = false;
        }
    }
}
