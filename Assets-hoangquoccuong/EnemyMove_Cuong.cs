using UnityEngine;

public class EnemyMove_Cuong : MonoBehaviour
{
    public float speed = 2f;
    public Transform edgeCheck;
    public LayerMask groundLayer;
    private bool movingRight = true;

    private bool canFlip = true; // Ngăn Flip liên tục

    void Update()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Raycast kiểm tra rìa
        RaycastHit2D hit = Physics2D.Raycast(edgeCheck.position, Vector2.down, 0.2f, groundLayer);
        Debug.DrawRay(edgeCheck.position, Vector2.down * 0.2f, Color.red);

        if (!hit && canFlip)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        // Lật hướng hiển thị
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Lật vị trí edgeCheck
        Vector3 localPos = edgeCheck.localPosition;
        localPos.x *= -1;
        edgeCheck.localPosition = localPos;

        // Ngăn Flip trong 0.3 giây
        canFlip = false;
        Invoke(nameof(EnableFlip), 0.3f);
    }

    void EnableFlip()
    {
        canFlip = true;
    }
}
