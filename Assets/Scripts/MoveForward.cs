using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool moveLeft = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float direction = moveLeft ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    public void Flip()
    {
        moveLeft = !moveLeft;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
